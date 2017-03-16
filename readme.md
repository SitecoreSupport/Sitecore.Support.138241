# Sitecore.Support.138241
The `InvalidOperationException` error occurs when the `FormID` or `Data Source` field is empty on the `Mvc Form` rendering in `Layout Details`.<br/>
After applying this patch the empty form (which ID is defined in the `Sitecore.Support.138241.config` file) with additional information will be displayed instead of the error.<br/>
The default item path for the empty form is `/sitecore/system/Modules/Web Forms for Marketers/Sample forms/Empty form`.<br/>
It can be customized depending on special needs or it is possible to select another form using the `WFM.EmptyForm` setting in the mentioned configuration file.

## License  
This patch is licensed under the [Sitecore Corporation A/S License for GitHub](https://github.com/sitecoresupport/Sitecore.Support.138241/blob/master/LICENSE).  

## Download  
Downloads are available via [GitHub Releases](https://github.com/sitecoresupport/Sitecore.Support.138241/releases).  

[![Github All Releases](https://img.shields.io/github/downloads/SitecoreSupport/Sitecore.Support.138241/total.svg)](https://github.com/SitecoreSupport/Sitecore.Support.138241/releases)
