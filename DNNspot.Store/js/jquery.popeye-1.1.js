/*
 * jQuery Popeye 1.1 - http://dev.herr-schuessler.de/jquery/popeye/
 *
 * converts a HTML image list in image gallery with inline enlargement
 *
 * Copyright (C) 2008 - 2010 Christoph Schuessler (schreib@herr-schuessler.de)
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 */
 
(function($) {


    ////////////////////////////////////////////////////////////////////////////
    //
    // jQuery.fn.popeye
    // popeye definition
    //
    ////////////////////////////////////////////////////////////////////////////
    jQuery.fn.popeye = function(options) {

        // cache the object
        //----------------------------------------------------------------------
        var obj = jQuery(this);

        // build main options before element iteration
        //----------------------------------------------------------------------
        var opts = jQuery.extend({}, jQuery.fn.popeye.defaults, options);


        ////////////////////////////////////////////////////////////////////////////////
        //
        // firebug console output
        // @param text String the debug message
        // @param type String the message type [error | info | warn | debug] (optional)
        //
        ////////////////////////////////////////////////////////////////////////////////
        function debug(text, type) {
            if (window.console && window.console.log && opts.debug) {
                if (type == 'error' && window.console.error) {
                    window.console.error(text);
                }
                else if (type == 'info' && window.console.info) {
                    window.console.info(text);
                }
                else if (type == 'warn' && window.console.warn) {
                    window.console.warn(text);
                }
                else if (type == 'debug' && window.console.debug) {
                    window.console.debug(text);
                }
                else {
                    window.console.log(text);
                }
            }
        }
        function debugGroup(text) {
            if (window.console && window.console.groupCollapsed && opts.debug) {
                window.console.groupCollapsed(text);
            }
        }
        function debugGroupEnd() {
            if (window.console && window.console.groupEnd && opts.debug) {
                window.console.groupEnd();
            }
        }

        // let's go!
        //----------------------------------------------------------------------
        return this.each(function() {

            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.showThumb
            // show thumbnail
            // @param i Int the index of the thumbnail to show (optional)
            // @param transition Bool show transition between images (optional)
            //
            ////////////////////////////////////////////////////////////////////
            function showThumb(i, transition) {

                // optional parameters
                transition = transition || false;
                i = i || cur;

                // set selected thumb as background image of stage
                var cssStageImage = {
                    backgroundImage: 'url(' + im.small[i] + ')'
                };

                // if we are in enlarged mode, return to thumb mode
                if (enlarged) {

                    enlarged = false;
                    debug('jQuery.fn.showThumb.compact: Entering COMPACT MODE', 'info');

                    // hide caption during animation
                    hideCaption(true);

                    // fade image out and compact stage with transition
                    ppyStage.fadeTo((opts.duration / 2), 0).animate(cssCompactStage, {
                        queue: false,
                        duration: opts.duration,
                        easing: opts.easing,
                        complete: function() {

                            // remove extra styling
                            obj.removeClass(eclass);

                            // switch buttons
                            ppySwitchEnlarge.removeClass('ppy-hidden');
                            ppySwitchCompact.addClass('ppy-hidden');

                            // recursive function call
                            showThumb();

                            // fade the stage back in
                            jQuery(this).fadeTo((opts.duration / 2), 1);
                        }
                    });
                }
                else {

                    // if we navigate from one image to the next, fade out the stage
                    if (transition) {

                        // fade out image so that background shines through
                        // background can contain loading gfx
                        ppyStageWrap.addClass(lclass);
                        ppyStage.fadeTo((opts.duration / 2), 0);

                        // once thumb has loadded...
                        var thumbPreloader = new Image();
                        thumbPreloader.onload = function() {

                            debug('jQuery.fn.popeye.showThumb: Thumbnail ' + i + ' loaded', 'info');

                            // remove loading indicator
                            ppyStageWrap.removeClass(lclass);

                            // add all upcoming animations to the queue so that 
                            // they won't start when the preolader has loaded but when the fadeOut has finished
                            ppyStage.animate(cssStageImage, 0, function() {

                                // set the new image
                                ppyStage.css(cssStageImage);

                                // fade the stage back in
                                jQuery(this).fadeTo((opts.duration / 2), 1);

                                // update counter and caption
                                if (opts.caption == 'hover') {
                                    showCaption(im.title[i]);
                                }
                                else if (opts.caption == 'permanent') {
                                    updateCaption(im.title[i]);
                                }
                                updateCounter();
                            });

                            //  fix IE animated gif bug
                            thumbPreloader.onload = function() { };
                        };
                        // preload thumb
                        thumbPreloader.src = im.small[i];
                    }

                    // or just drag the image to the stage
                    else {
                        ppyStage.css(cssStageImage);
                        updateCounter();
                        updateCaption(im.title[i]);
                    }

                    // preload big image for instant availability
                    var preloader = new Image();
                    preloader.onload = function() {
                        debug('jQuery.fn.popeye.showThumb: Image ' + i + ' loaded', 'info');
                        preloader.onload = function() { };
                    };
                    preloader.src = im.large[i];

                }
            }


            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.showImage
            // show large image
            // @param i Int the index of the image to show (optional)
            //
            ////////////////////////////////////////////////////////////////////
            function showImage(i) {

                // optional parameter i
                i = i || cur;

                enlarged = true;
                debug('jQuery.fn.popeye.showImage: Entering ENLARGED MODE', 'info');

                // fade out image so that background shines through
                // background can contain loading gfx
                ppyStageWrap.addClass(lclass);
                ppyStage.fadeTo((opts.duration / 2), 0);

                // add extra class, expanded box can be styled accordingly
                obj.addClass(eclass);

                // once image has loadded...
                var preloader = new Image();
                preloader.onload = function() {

                    // remove loading class
                    ppyStageWrap.removeClass(lclass);

                    // set css
                    var cssStageTo = {
                        width: preloader.width,
                        height: preloader.height
                    };
                    var cssStageIm = {
                        backgroundImage: 'url(' + im.large[i] + ')',
                        backgroundPosition: 'left top'
                    };

                    // hide caption during animation
                    hideCaption();

                    // show transitional animation
                    ppyStage.animate(cssStageTo, {
                        queue: false,
                        duration: opts.duration,
                        easing: opts.easing,
                        complete: function() {

                            // switch buttons
                            ppySwitchCompact.removeClass('ppy-hidden');
                            ppySwitchEnlarge.addClass('ppy-hidden');

                            updateCounter();

                            // set new bg image and fade it in
                            jQuery(this).css(cssStageIm).fadeTo((opts.duration / 2), 1);

                            // show caption TODO: hover / permanent?
                            showCaption(im.title[i]);

                            preloadNeighbours();
                        }
                    });
                };

                // preload image
                preloader.src = im.large[i];
            }


            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.updateCounter
            // update image counter
            // @param i Int the index of the image (optional)
            //
            ////////////////////////////////////////////////////////////////////
            function updateCounter(i) {

                // optional parameter
                i = i || cur;

                ppyTotal.text(tot);        // total images
                ppyCurrent.text(i + 1);    // current image number
                debug('jQuery.fn.popeye.updateCounter: Displaying image ' + (i + 1) + ' of ' + tot);
            }

            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.preloadNeighbours
            // preload next and previos image
            // @param i Int the index of the current image (optional)
            //
            ////////////////////////////////////////////////////////////////////
            function preloadNeighbours(i) {

                // optional parameter
                i = i || cur;

                var preloaderNext = new Image();
                var preloaderPrev = new Image();

                var neighbour = i;

                // next image
                if (neighbour < (tot - 1)) {
                    neighbour++;
                } else {
                    neighbour = 0;
                }
                preloaderNext.src = im.large[neighbour];

                // previous image
                neighbour = i;
                if (neighbour <= 0) {
                    neighbour = tot - 1;
                } else {
                    neighbour--;
                }
                preloaderPrev.src = im.large[neighbour];
            }


            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.showNav
            //
            ////////////////////////////////////////////////////////////////////
            function showNav() {
                ppyNav.stop().fadeTo(150, opts.opacity);
            }


            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.hideNav
            //
            ////////////////////////////////////////////////////////////////////
            function hideNav() {
                ppyNav.stop().fadeTo(150, 0);
            }


            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.updateCaption
            // 
            //
            ////////////////////////////////////////////////////////////////////
            function updateCaption(caption) {

                if (opts.caption !== false) {
                    // update text box
                    ppyText.text(caption);
                }
            }

            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.showCaption
            // 
            //
            ////////////////////////////////////////////////////////////////////
            function showCaption(caption) {

                // if caption string is not empty...
                if (caption && opts.caption) {
                    updateCaption(caption);

                    debug('jQuery.fn.popeye.showCaption -> ppyCaptionWrap.outerHeight(true): ' + ppyCaptionWrap.outerHeight(true));

                    // make caption box visible
                    var cssPpyCaption = {
                        visibility: 'visible'
                    };
                    ppyCaption.css(cssPpyCaption);

                    // and animate it to its childs height
                    ppyCaption.animate({ 'height': ppyCaptionWrap.outerHeight(true) }, {
                        queue: false,
                        duration: 90,
                        easing: opts.easing
                    });
                }
                // if there's no caption to show...
                else if (!caption) {
                    hideCaption();
                }

            }


            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.hideCaption
            // 
            //
            ////////////////////////////////////////////////////////////////////
            function hideCaption() {

                // css to hide caption but allow its inner text box to expand to content height
                var cssPpyCaption = {
                    visibility: 'hidden',
                    overflow: 'hidden'
                };

                // slide up caption box and hide it when done
                ppyCaption.animate({ 'height': '0px' }, {
                    queue: false,
                    duration: 90,
                    easing: opts.easing,
                    complete: function() {
                        ppyCaption.css(cssPpyCaption);
                    }
                });
            }

            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.previous
            // show previous image
            //
            ////////////////////////////////////////////////////////////////////
            function previous() {
                if (cur <= 0) {
                    cur = tot - 1;
                } else {
                    cur--;
                }
                if (enlarged) {
                    showImage(cur);
                }
                else {
                    //showThumb(cur, true); // IE 8 blows up with transitions enabled :(
                    showThumb(cur, false);
                }
                return cur;
            }

            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.next
            // show next image
            //
            ////////////////////////////////////////////////////////////////////
            function next() {
                if (cur < (tot - 1)) {
                    cur++;
                } else {
                    cur = 0;
                }
                if (enlarged) {
                    showImage(cur);
                }
                else {
                    //showThumb(cur, true); // IE 8 blows up with transitions enabled :(
                    showThumb(cur, false);
                }
                return cur;
            }


            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.init
            // setup of popeye DOM and events
            //
            ////////////////////////////////////////////////////////////////////
            function init() {

                // get thumbnail dims and calculate max stage dims
                debugGroup('jQuery.fn.popeye.init: Image dimensions');
                obj.find('.ppy-imglist li').each(function(i) {
                    im.width[i] = jQuery(this).find('img').width();       // the image width
                    im.height[i] = jQuery(this).find('img').height();     // the image height
                    debug('jQuery.fn.popeye.init -> im.width[' + i + ']: ' + im.width[i] + ', im.height[' + i + ']: ' + im.height[i]);

                });
                debugGroupEnd();

                // CSS: set tools height to stage height and hide it
                cssPpyNav = {
                    opacity: 0
                };

                // popeye dom setup
                //--------------------------------------------------------------

                // dispose of original image list
                obj.find('.ppy-imglist').remove();


                var pHeight = obj.outerHeight();
                var pWidth = obj.outerWidth();
                var sHeight = ppyStage.height();
                var sWidth = ppyStage.width();

                if (opts.caption == 'hover' || false) {
                    pHeight = ppyOuter.outerHeight();
                    pWidth = ppyOuter.outerWidth();
                }

                // set placeholder to the dimensions of popeye box
                var cssPlaceholder = {
                    height: pHeight,
                    width: pWidth,
                    float: obj.css('float'),
                    marginTop: obj.css('margin-top'),
                    marginRight: obj.css('margin-right'),
                    marginBottom: obj.css('margin-bottom'),
                    marginLeft: obj.css('margin-left')
                };

                // save original stage dimensions for later
                cssCompactStage = {
                    height: sHeight,
                    width: sWidth
                };

                // make popeye box absolutely positioned within placeholder
                var cssPopeye = {
                    margin: 0,
                    position: 'absolute',
                    top: 0,
                    width: 'auto',
                    height: 'auto'
                };
                if (opts.direction == 'right') {
                    cssPopeye.left = 0;
                }
                if (opts.direction == 'left') {
                    cssPopeye.right = 0;
                }

                // add css 
                ppyPlaceholder.css(cssPlaceholder);
                obj.css(cssPopeye);

                // wrap popeye in placeholder 
                obj.wrap(ppyPlaceholder);

                // wrap stage in container for extra styling (e.g. loading gfx)
                ppyStageWrap = ppyStage.wrap(ppyStageWrap).parent();

                // wrap caption contents in wrapper (can't use wrap() here...)
                ppyCaptionWrap = ppyCaption.wrapInner(ppyCaptionWrap).children().eq(0);

                // display first image
                showThumb();


                // add event handlers
                //--------------------------------------------------------------

                // hover behaviour for navigation
                if (opts.navigation == 'hover') {
                    hideNav();
                    obj.hover(
                        function() {
                            showNav();
                        },
                        function() {
                            hideNav();
                        }
                    );
                    ppyNav.hover(
                        function() {
                            showNav();
                        },
                        function() {
                            hideNav();
                        }
                    );
                }

                // hover behaviour for caption
                if (opts.caption == 'hover') {
                    hideCaption();
                    obj.hover(
                        function() {
                            showCaption(im.title[cur]);
                        },
                        function() {
                            hideCaption(true);
                        }
                    );
                }

                // previous image button
                ppyPrev.click(previous);

                // next image button
                ppyNext.click(next);

                // enlarge image button
                ppySwitchEnlarge.click(function() {
                    showImage();
                    return false;
                });

                // compact image button                          
                ppySwitchCompact.click(function() {
                    showThumb(cur);
                    return false;
                });

            }


            ////////////////////////////////////////////////////////////////////
            //
            // initial setup
            //
            ////////////////////////////////////////////////////////////////////

            // popeye vars
            //------------------------------------------------------------------

            // image preloaders
            var preloaders = [];

            // html nodes
            var ppyPlaceholder = jQuery('<div class="ppy-placeholder"></div>');
            var ppyOuter = obj.find('.ppy-outer');
            var ppyStage = obj.find('.ppy-stage');
            var ppyStageWrap = jQuery('<div class="ppy-stagewrap"></div>');
            var ppyNav = obj.find('.ppy-nav');
            var ppyPrev = obj.find('.ppy-prev');
            var ppyNext = obj.find('.ppy-next');
            var ppySwitchEnlarge = obj.find('.ppy-switch-enlarge');
            var ppySwitchCompact = obj.find('.ppy-switch-compact');
            var ppyCaption = obj.find('.ppy-caption');
            var ppyCaptionWrap = jQuery('<div class="ppy-captionwrap"></div>');
            var ppyText = obj.find('.ppy-text');
            var ppyCounter = obj.find('.ppy-counter');
            var ppyCurrent = obj.find('.ppy-current');
            var ppyTotal = obj.find('.ppy-total');

            // declare image object arrays
            var im = {
                small: [],
                title: [],
                large: [],
                width: [],
                height: []
            };

            // counter vars
            var cur = 0;                        // array index of currently displayed image
            var tot = obj.find('img').length;   // total number of images
            var togo = tot;
            var singleImageMode = false;
            if (tot === 0) {
                singleImageMode = true;
            }
            debug('jQuery.fn.popeye -> ' + tot + ' thumbnails found.');

            // declare CSS vars
            var cssCompactStage = {};
            var cssPpyNav = {};

            var eclass = 'ppy-expanded';     //class to be applied to enlarged popeye-box
            var lclass = 'ppy-loading';      //class to be applied to stage while loading image

            // popeye starts of in compact mode
            var enlarged = false;
            ppySwitchCompact.addClass('ppy-hidden');

            // preload all thumbs
            //--------------------------------------------------------------
            debugGroup('jQuery.fn.popeye: Image preloader');
            obj.find('li').each(function(i) {

                im.small[i] = jQuery(this).find('img').attr('src');   // the thumbnail url
                im.title[i] = jQuery(this).find('img').attr('alt');   // the image title
                im.large[i] = jQuery(this).find('a').attr('href');    // the image url
                debug('jQuery.fn.popeye -> Loading "' + im.small[i] + '"');

                // call init after all images have loaded. TODO: don't wait for all thumbs to load
                jQuery(this).find('img').load(function() {

                    // if image that has loaded is last image in list
                    if (--togo < 1) {
                        debug('jQuery.fn.popeye -> All thumbnails loaded!');
                        init();
                    }
                }).attr('src', im.small[i]);
            });
            debugGroupEnd();
        });
    };

    ////////////////////////////////////////////////////////////////////////////
    //
    // jQuery.fn.popeye.defaults
    // set default  options
    //
    ////////////////////////////////////////////////////////////////////////////
    jQuery.fn.popeye.defaults = {

        navigation: 'hover',            //visibility of navigation - can be 'permanent' or 'hover'
        caption: 'hover',            //visibility of caption, based on image title - can be false, 'permanent' or 'hover'

        dlclass: 'ppy-left',         //class to be applied to ppy-tools if popeye opens to the left
        drclass: 'ppy-right',        //class to be applied to ppy-tools if popeye opens to the right

        direction: 'right',            //direction that popeye-box opens, can be 'left' or 'right'
        duration: 240,                //duration of transitional effect when enlarging or closing the box
        opacity: 0.7,                //opacity of navigational overlay (only applicable if 'navigation' is set to 'hover'
        easing: 'swing',            //easing type, can be 'swing', 'linear' or any of jQuery Easing Plugin types (Plugin required)

        debug: false               //turn on console output (slows down IE8!)

    };

    // end of closure, bind to jQuery Object
})(jQuery); 


////////////////////////////////////////////////////////////////////////////////
//
// avoid content flicker for non-js user agents
// (in order to use this, the js-files have to be included in the head of the
// html file!)
//
////////////////////////////////////////////////////////////////////////////////
jQuery('head').append('<style type="text/css"> .ppy-imglist { position: absolute; top: -1000em; left: -1000em; } </style>');
