//var urlToAjaxHandler = "<%= StoreUrls.AdminAjaxHandler %>";
var urlToAjaxHandler = "/DesktopModules/DNNspot-Store/Modules/Admin/AjaxHandler.ashx";

jQuery(function($) {

    var sortableOptions = {};
    sortableOptions.handle = ".moveHandle";
    sortableOptions.axis = "y";
    sortableOptions.placeholder = "ui-drop-placeholder";
    sortableOptions.tolerance = "pointer";
    sortableOptions.opacity = 0.5;
    sortableOptions.cursorAt = "bottom";
    sortableOptions.sort = function(event, ui) {
        var itemHeightPx = ui.item.css('height');
        if (itemHeightPx) {
            ui.placeholder.css('height', itemHeightPx);
        }
    };
    sortableOptions.update = function(event, ui) {

        var $theList = jQuery(this);

        var sortedArray = $theList.sortable('toArray');
        var catIdArray = [];
        jQuery.each(sortedArray, function(i, val) {
            catIdArray.push(parseInt(val.replace("catId-", "")));
        });
        if (catIdArray.length > 1) {
            jQuery.post(urlToAjaxHandler, { 'action': 'updateCategorySortOrder', 'sortedCategoryIds': catIdArray }, function(data) {
                if (data.success) {
                    $theList.effect('highlight', { backgroundColor: '#FFFF40' }, 1500);
                }
                else {
                    //console.log('error');
                    //if (data.error) console.log(data.error);
                }
            }
                , "json");
        }
    };

    // level 0
    var $rootCats = jQuery("#categories .catDivs");
    $rootCats.sortable(sortableOptions);
    $rootCats.disableSelection();

    // level 1 div's
    var $level1Cats = jQuery("#categories .catDivs > div > div");
    $level1Cats.sortable(sortableOptions);
    $level1Cats.disableSelection();

    // level 2 div's
    var $level2Cats = jQuery("#categories .catDivs div.level1 > div");
    $level2Cats.sortable(sortableOptions);
    $level2Cats.disableSelection();

    // level 3 div's
    var $level3Cats = jQuery("#categories .catDivs div.level2 > div");
    $level3Cats.sortable(sortableOptions);
    $level3Cats.disableSelection();

    // level 4 div's
    var $level4Cats = jQuery("#categories .catDivs div.level3 > div");
    $level4Cats.sortable(sortableOptions);
    $level4Cats.disableSelection();

    // level 5 div's
    var $level5Cats = jQuery("#categories .catDivs div.level4 > div");
    $level5Cats.sortable(sortableOptions);
    $level5Cats.disableSelection();
});