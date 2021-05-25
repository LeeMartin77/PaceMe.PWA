# PaceMe Training Planner - Frontend Blazor PWA

[![Build Status](https://dev.azure.com/LeeJohnMartin/PaceMe/_apis/build/status/LeeMartin77.PaceMe.PWA?branchName=master)](https://dev.azure.com/LeeJohnMartin/PaceMe/_build/latest?definitionId=10&branchName=master)

This project is a Blazor PWA, working as the frontend for a run training planner. It has a few key goals:

- Something to learn the ins-and-outs of Blazor with, as well as PWAs
- Be useful to me in my day-to-day as a keen runner, trying to elevate myself past a google sheet with my plans in
- Something fun to tinker with in the more general UX/UI sense

## Development

For development, this is expected to be run on linux, in a VS Code dev container. You'll want to populate ```wwwroot/appsettings.Development.json``` with the needed information - AzureADB2C configuration and the expected function app API endpoint. Once this is done, you can run the app with:

```dotnet watch run --urls "http://0.0.0.0:5000;https://0.0.0.0:5001"```

## Credits?

Frontend framework being used:
- Milligram: [https://milligram.io/](https://milligram.io/)
