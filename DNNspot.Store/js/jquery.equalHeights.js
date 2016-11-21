﻿/*-------------------------------------------------------------------- 
* JQuery Plugin: "EqualHeights"
* by:	Scott Jehl, Todd Parker, Maggie Costello Wachs (http://www.filamentgroup.com)
*
* Copyright (c) 2008 Filament Group
* Licensed under GPL (http://www.opensource.org/licenses/gpl-license.php)
*
* Description: Compares the heights or widths of the top-level children of a provided element 
and sets their min-height to the tallest height (or width to widest width). Sets in em units 
by default if pxToEm() method is available.
* Dependencies: jQuery library, pxToEm method	(article: 
http://www.filamentgroup.com/lab/retaining_scalable_interfaces_with_pixel_to_em_conversion/)							  
* Usage Example: jQuery(element).equalHeights();
Optional: to set min-height in px, pass a true argument: jQuery(element).equalHeights(true);
* Version: 2.0, 08.01.2008
--------------------------------------------------------------------*/

jQuery.fn.equalHeights = function(px) {
    jQuery(this).each(function() {
        var currentTallest = 0;
        jQuery(this).children().each(function(i) {
            //console.log(jQuery(this).height());
            //alert(jQuery(this).height());
            if (jQuery(this).height() > currentTallest) { currentTallest = jQuery(this).height(); }
        });
        //if (!px || !Number.prototype.pxToEm) currentTallest = jQuery(currentTallest).toEm(); //use ems unless px is specified
        // for ie6, set height since min-height isn't supported
        if (jQuery.browser.msie) {
            var ieVersion = jQuery.browser.version;            
            if (ieVersion == 6.0) {
                jQuery(this).children().css({ 'height': currentTallest });
            }
        }
        jQuery(this).children().css({ 'min-height': currentTallest });
    });
    return this;
};