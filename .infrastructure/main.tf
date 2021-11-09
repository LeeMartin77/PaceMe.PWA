terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=2.84.0"
    }
  }
  backend "azurerm" {
    resource_group_name  = "PaceMe.GlobalResources"
    storage_account_name = "pacemeterraform"
    container_name       = "tfstate"
  }
}

variable "environment" {
  type = string
}

variable "domain_prefix" {
  type    = string
}

variable "regions" {
  type = map(string)
  default = {
    "primary" = "UK South"
    "cdn"     = "westeurope"
  }
}

provider "azurerm" {
   features {}
}

resource "azurerm_resource_group" "pacemefrontend" {
  name     = join("", ["PaceMe.Frontend.", var.environment])
  location = "UK South"
}


resource "azurerm_storage_account" "app_storage" {
  name                     = "apppacemeinfo${var.environment}"
  resource_group_name      = azurerm_resource_group.pacemefrontend.name
  location                 = azurerm_resource_group.pacemefrontend.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
  account_kind             = "StorageV2"

  static_website {
    index_document     = "index.html"
    error_404_document = "index.html"
  }
}

resource "azurerm_cdn_profile" "cdn" {
  name                = "apppaceme-cdn-${var.environment}"
  location            = var.regions["cdn"]
  resource_group_name = azurerm_resource_group.pacemefrontend.name
  sku                 = "Standard_Microsoft"
}

resource "azurerm_cdn_endpoint" "cdn_app" {
  name                = "apppaceme-cdn-endpoint-${var.environment}"
  profile_name        = azurerm_cdn_profile.cdn.name
  location            = azurerm_cdn_profile.cdn.location
  resource_group_name = azurerm_resource_group.pacemefrontend.name
  origin_host_header  = azurerm_storage_account.app_storage.primary_web_host

  origin {
    name      = "storage-account-endpoint"
    host_name = azurerm_storage_account.app_storage.primary_web_host
  }

  delivery_rule {
    name  = "EnforceHTTPS"
    order = "1"

    request_scheme_condition {
      operator     = "Equal"
      match_values = ["HTTP"]
    }

    url_redirect_action {
      redirect_type = "Found"
      protocol      = "Https"
    }
  }
}


data "azurerm_dns_zone" "pacemeinfo" {
  name                = "paceme.info"
  resource_group_name = "PaceMe.GlobalResources"
}

resource "azurerm_dns_cname_record" "pacemeinfo" {
  name                = var.domain_prefix
  zone_name           = data.azurerm_dns_zone.pacemeinfo.name
  resource_group_name = data.azurerm_dns_zone.pacemeinfo.resource_group_name
  ttl                 = 3600
  target_resource_id  = azurerm_cdn_endpoint.cdn_app.id
}

resource "azurerm_cdn_endpoint_custom_domain" "pacemeinfo" {
  name            = var.domain_prefix
  cdn_endpoint_id = azurerm_cdn_endpoint.cdn_app.id
  host_name       = "${azurerm_dns_cname_record.pacemeinfo.name}.${data.azurerm_dns_zone.pacemeinfo.name}"

  provisioner "local-exec" {
    command = <<EOT
    az cdn custom-domain enable-https \
    --endpoint-name ${azurerm_cdn_endpoint.cdn_app.name} \
    --resource-group ${azurerm_resource_group.pacemefrontend.name} \
    --profile-name ${azurerm_cdn_profile.cdn.name} \
    -n "${azurerm_cdn_endpoint_custom_domain.pacemeinfo.name}"
    EOT
  }
}