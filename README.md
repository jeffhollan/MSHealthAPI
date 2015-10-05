# Microsoft Health API App
[![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/)

This API App will allow you to authenticate against the Microsoft Health service and perform calls to retreive data from their API.

## Prerequisites ##

Because this API requires Authentication, and Microsoft Health is not one of the APIs supported out-of-the-box by API Apps Gateway, the app itself will manage authentication and tokens.  Because of this, it is necessary to have a registered application with Microsoft Health to authorize the calls.

You can register your application here: [Microsoft Account Applications](http://go.microsoft.com/fwlink/?LinkId=193157)

When you provision this app you will give it a site name.  You will need this in order to register your application for the redirect URL.  If you registered this API as `MSHealthAPI9a2kd892` your redirect URL would be `https://MSHealthAPI9a2kd892.azurewebsites.net/redirect`.

## Deploying ##

This API App will deploy a Resource Group, Gateway, API App Host, and API App.  Click the "Deploy To Azure" button above to deploy.  You will need the Client ID and Client Secret from the application you registered with Microsoft Account.

### Authorizing ###

After deploying, navigate to the API App URL and click the "Authorize" button.  If your Client ID, Secret, and the registered Redirect URL are all correct you should get back a `Successfully Authorized` response.  The API App will manage refresh tokens, so **be sure to change Application Settings for the API App to 'Internal'** after Authentication.  If not, you will have an open endpoint to your Microsoft Health API data.

If you are getting errors, you can use the `/showredirect` URL on your API App to have it return the redirect URL it has registered based on deployment URL.  Make sure this matches the redirect URL in your Microsoft Developer Account registration.

## Triggers and Actions ##

### Triggers ###

This API has the following triggers:

| Name | Description |
| ---- | ----------- |
| Trigger On Device Sync | Will trigger and return the last full hour of data *only if* a Microsoft Band has sync'd its data for that hour |
| Trigger On Activity | Will trigger and return activity summaries whenever a new activity is added (e.g. Run is completed and sync'd) |
| Get Hourly Summary | Will return the last full hour of data, however unlike 'Trigger on Device Sync' the data will be returned even if the Band hasn't sync'd |

### Actions ###

This API has a single action:

| Name | Description |
| --- | -----|
| Get Activities | Returns an array of activities (e.g. Run, Bike, etc.) within the Start Time and End Time specified |

### Data Structure ###

Please refer to the [Microsoft Band Cloud API Preview](http://developer.microsoftband.com/cloudAPI) documentation for information on the data elements returned.

## Using with Power BI ##

If you are using this with the Power BI API App, this repository also contains a 'Power BI Dataset Schema.json' file which can be used with the Power BI REST API to create the dataset needed for Microsoft Health data.  There is also a 'LogicAppDefinition.json' file with a sample definition of the Microsoft Health to Power BI Logic App.