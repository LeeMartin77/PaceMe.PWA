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
    key                  = "local.terraform-frontend.tfstate"
  }
}

variable "environment" {
  type = string
  default = "localdev"
}

variable "domain" {
  type    = string
  default = "app.paceme.info"
}

variable "cdn_application_id" {
  default = "205478c0-bd83-4e1b-a9d6-db63a3e1e1c8" # This is azure's application UUID for a CDN endpoint
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
