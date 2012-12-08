# ImranB.ModelBindingFix

## Overview

This package will fix a model binding issue which available in ASP.NET MVC 4 and ASP.NET Web API.


## Example
Just add this namespace in global.asax.cs file, 

         ImranB.ModelBindingFix;


and then add this line in Application_Start method to fix the issue,

	Fixer.FixModelBindingIssue();
	// For fixing only in MVC call this
	//Fixer.FixMvcModelBindingIssue();
	// For fixing only in Web API call this
	//Fixer.FixWebApiModelBindingIssue();	


