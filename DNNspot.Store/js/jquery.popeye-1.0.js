/*
 * jQuery Popeye 1.0 - http://dev.herr-schuessler.de/jquery/popeye/
 *
 * converts a HTML image list in image gallery with inline enlargement
 *
 * Copyright (C) 2008,2009 Christoph Schuessler (schreib@herr-schuessler.de)
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

        // set context vars
        //----------------------------------------------------------------------
        var obj = jQuery(this);

        // build main options before element iteration
        //----------------------------------------------------------------------
        var opts = jQuery.extend({}, jQuery.fn.popeye.defaults, options);


        // firebug console output
        //----------------------------------------------------------------------
        function debug(msg) {
            if (window.console && window.console.log && opts.debug) {
                window.console.log(msg);
            }
        }


        ////////////////////////////////////////////////////////////////////////
        //
        // -> start
        // apply popeye to all calling instances
        //
        ////////////////////////////////////////////////////////////////////////
        return this.each(function() {


            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.display
            // display thumbnail on stage, update toolbar
            //
            ////////////////////////////////////////////////////////////////////
            function display(i, transition) {

                // optional parameter transition
                transition = transition || false;

                // set selected thumb as background image of stage
                var stageIm = {
                    backgroundImage: 'url(' + im.small[i] + ')',
                    backgroundPosition: 'center'
                };

                // if transition is desired, fade out, update counter and load
                if (transition) {
                    ppyStageWrap.addClass(opts.lclass);
                    ppyStage.fadeTo(100, 0, function() {
                        updateCounter(i);
                        ppyStage.css(stageIm);
                    });
                }
                else {
                    ppyStage.css(stageIm);
                    updateCounter(i);
                }

                // once thumb / img has loadded...
                //--------------------------------------------------------------
                preloader.onload = function() {

                    ppyStageWrap.removeClass(opts.lclass);

                    //if set, show transition on change of image
                    if (transition) {
                        ppyStage.fadeTo(100, 1);
                        displayCaption(im.title[i]);     // show caption
                    }
                };

                // if popeye is in enlarged mode...
                //--------------------------------------------------------------
                if (enlarged) {
                    enlarge(i);
                }

                // if popeye is in compact mode...
                //--------------------------------------------------------------
                else {

                    // preload thumb
                    preloader.src = im.small[i];
                    debug('jQuery.fn.popeye.display: Thumbnail ' + i + ' loaded');

                    // preload big image for faster abailability
                    preloader2.onload = function() {
                        debug('jQuery.fn.popeye.display: Image ' + i + ' loaded');
                    };
                    preloader2.src = im.large[i];
                }
            }


            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.updateCounter
            // update image counter
            //
            ////////////////////////////////////////////////////////////////////
            function updateCounter(i) {
                ppyTotal.text(' ' + tot);        // total images
                ppyCur.text((i + 1) + ' ');    // current image number
                debug('jQuery.fn.popeye.updateCounter: Displaying image ' + (i + 1) + ' of ' + tot);
            }

            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.preloadAdjacent
            // preload next and previos image
            //
            ////////////////////////////////////////////////////////////////////
            function preloadAdjacent(i) {

                // preload next and previos image
                var next = i;
                if (next < (tot - 1)) {
                    next++;
                } else {
                    next = 0;
                }
                preloaderNext.onload = function() {
                    debug('jQuery.fn.popeye.preloadAdjacent: Next image (' + next + ') loaded');
                };
                preloaderNext.src = im.large[next];

                var prev = i;
                if (prev <= 0) {
                    prev = tot - 1;
                } else {
                    prev--;
                }
                preloaderPrev.onload = function() {
                    debug('jQuery.fn.popeye.preloadAdjacent: Previous image (' + prev + ') loaded');
                };
                preloaderPrev.src = im.large[prev];
            }


            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.enlarge
            // enlarge popeye
            //
            ////////////////////////////////////////////////////////////////////
            function enlarge(i) {

                enlarged = true;
                debug('jQuery.fn.popeye.enlarge: Entering ENLARGED MODE');

                // fade out image so that background shines through
                // background can contain loading gfx
                ppyStageWrap.addClass(opts.lclass);
                ppyStage.fadeTo((opts.duration / 2), 0);
                obj.addClass(opts.eclass);


                // preload image and display it with transition
                //--------------------------------------------------------------

                // once image has loadded...
                preloader.onload = function() {

                    // remove loading class
                    ppyStageWrap.removeClass(opts.lclass);

                    // get image dimensions
                    var imWidth = preloader.width;
                    var imHeight = preloader.height;

                    // set css
                    var cssStageTo = {
                        width: imWidth,
                        height: imHeight
                    };
                    var cssStageIm = {
                        backgroundImage: 'url(' + im.large[i] + ')',
                        backgroundPosition: 'left top'
                    };

                    // hide caption during animation
                    hideCaption(true);

                    // show transitional animation
                    ppyStage.animate(cssStageTo, {
                        queue: false,
                        duration: opts.duration,
                        easing: opts.easing,
                        complete: function() {

                            // switch button clases
                            ppySwitch.removeClass('ppy-enlarge');
                            ppySwitch.addClass('ppy-compact');
                            ppySwitch.html(opts.clabel);

                            // add compact function to switch                           
                            ppySwitch.unbind('click');
                            ppySwitch.click(function() {
                                compact(cur);
                                return false;
                            });

                            // add compact function to entire stage 
                            ppyStageWrap.click(function() {
                                compact(cur);
                                return false;
                            });
                            ppyStageWrap.attr('title', opts.clabel);

                            updateCounter(i);

                            // set new bg image and fade it in
                            jQuery(this).css(cssStageIm).fadeTo((opts.duration / 2), 1);

                            // show caption
                            displayCaption(im.title[i]);

                            preloadAdjacent(i);
                        }
                    });
                };
                // preload image AFTER calling onload
                //--------------------------------------------------------------
                preloader.src = im.large[i];
            }


            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.compact
            // compact popeye
            //
            ////////////////////////////////////////////////////////////////////
            function compact(i) {

                enlarged = false;
                debug('jQuery.fn.popeye.compact: Entering COMPACT MODE');

                // hide caption during animation
                hideCaption(true);

                // fade image out and compact stage with transition
                ppyStage.fadeTo((opts.duration / 2), 0).animate(cssCompactStage, {
                    queue: false,
                    duration: opts.duration,
                    easing: opts.easing,
                    complete: function() {

                        // return to original state
                        obj.removeClass(opts.eclass);

                        // switch button clases
                        ppySwitch.addClass('ppy-enlarge');
                        ppySwitch.removeClass('ppy-compact');
                        ppySwitch.html(opts.blabel);

                        // add enlarge event to switch                           
                        ppySwitch.unbind('click');
                        ppySwitch.click(function() {
                            enlarge(cur);
                            return false;
                        });

                        // remove compact function from entire stage
                        ppyStageWrap.removeAttr('title');
                        ppyStageWrap.unbind('click');

                        // show thumbnail image
                        display(cur);

                        // fade image in
                        jQuery(this).fadeTo((opts.duration / 2), 1, function() {

                            // show caption again
                            displayCaption(im.title[i]);
                        });
                    }
                });
            }


            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.displayCaption
            // 
            //
            ////////////////////////////////////////////////////////////////////
            function displayCaption(cap) {

                // update text box
                ppyText.text(cap);
                if (opts.countpos == 'caption') {
                    updateCounter(cur);
                }

                // if caption string is not empty...
                if (cap) {
                    // make caption box visible and set it to width of stage
                    var cssPpyCaption = {
                        visibility: 'visible',
                        width: ppyStage.outerWidth()
                    };
                    ppyCap.css(cssPpyCaption);


                    // slide caption box to height of inner text box
                    ppyCap.animate({ "height": ppyTextWrap.outerHeight() }, {
                        queue: false,
                        duration: 90,
                        easing: opts.easing
                    });
                }
                // if there's no caption, hide caption box completely to prevent
                // additional padding or margins of empty caption box to show
                else {
                    hideCaption(true);
                }
                //debug('jQuery.fn.popeye.displayCaption -> ppyTextWrap.outerHeight(): ' + ppyTextWrap.outerHeight() + ', ppyStage.outerWidth(): ' + ppyStage.outerWidth());
            }


            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.hideCaption
            // 
            //
            ////////////////////////////////////////////////////////////////////
            function hideCaption(transition) {

                // optional parameter transition
                transition = transition || false;

                // css to hide caption but allow its inner text box to expand to content height
                var cssPpyCaption = {
                    visibility: 'hidden',
                    overflow: 'hidden',
                    // set width to width of compact stage
                    // to allow enlarged box to transition to that width
                    width: maxWidth
                };

                // set animation speed to 0 if transition is not desired
                var duration = false;
                if (transition) {
                    duration = 70;
                }
                else {
                    duration = 0;
                }

                // slide up caption box and hide it when done
                ppyCap.animate({ "height": '0px' }, {
                    queue: false,
                    duration: duration,
                    easing: opts.easing,
                    complete: function() {
                        ppyCap.css(cssPpyCaption);
                    }
                });
            }


            ////////////////////////////////////////////////////////////////////
            //
            // jQuery.fn.popeye.init
            // setup of popeye DOM and events
            //
            ////////////////////////////////////////////////////////////////////
            function init() {

                // get thumbnail dims and calculate max stage dims
                obj.find('li').each(function(i) {
                    im.width[i] = jQuery(this).find('img').width();       // the image width
                    im.height[i] = jQuery(this).find('img').height();     // the image height
                    debug('jQuery.fn.popeye.init -> im.width[' + i + ']: ' + im.width[i] + ', im.height[' + i + ']: ' + im.height[i]);

                    //calculate minimum stage size
                    if (maxWidth > im.width[i]) {
                        maxWidth = im.width[i];
                    }
                    if (maxHeight > im.height[i]) {
                        maxHeight = im.height[i];
                    }
                });
                debug('jQuery.fn.popeye.init -> maxWidth: ' + maxWidth + ', maxHeight: ' + maxHeight);

                // CSS: compact stage dims
                cssCompactStage = {
                    width: maxWidth,
                    height: maxHeight
                };

                // CSS: set tools height to stage height and hide it                
                if (!opts.toolsAlwaysVisible) {    // Kevin Custom - check for new option
                    cssPpyTools = {
                        opacity: 0
                    };
                }

                // popeye dom setup
                //--------------------------------------------------------------
                debug('jQuery.fn.popeye.init -> Starting in COMPACT MODE');

                // dispose of original image list
                obj.find('ul').remove();

                // dispose of no-js warning
                if (opts.nojsclass) {
                    obj.find('.' + opts.nojsclass).remove();
                }

                // build popeye box DOM
                obj.append(ppyStageWrap);
                ppyStageWrap.append(ppyStage);
                ppyStage.append(ppyTools);
                if (opts.countpos == 'overlay') {
                    ppyStage.append(ppyCount);
                }
                ppyTools.append(ppyPrev);
                ppyTools.append(ppySwitch);
                ppyTools.append(ppyNext);
                if (opts.caption) {
                    ppyStageWrap.after(ppyCap);
                }

                // Kevin Custom
                ppyStageWrap.append(ppyTools);
                // END Kevin Custom

                ppyCount.append(ppyCur);
                ppyCount.append(ppyTotal);
                ppyCur.after(opts.oflabel);
                ppyCap.append(ppyTextWrap);
                if (opts.countpos == 'caption') {
                    ppyTextWrap.prepend(ppyCount);
                }
                ppyTextWrap.append(ppyText);

                // add class for additional styling
                if (opts.jsclass) {
                    obj.addClass(opts.jsclass);
                }

                // give tools a directional class
                if (opts.direction == 'left') {
                    ppyTools.addClass(opts.dlclass);
                }
                else if (opts.direction == 'right') {
                    ppyTools.addClass(opts.drclass);
                }

                // Style compact box
                ppyStage.css(cssCompactStage);
                ppyTools.css(cssPpyTools);

                // If we only have 1 image, hide the next/prev tools
                if (tot <= 1) {
                    ppyNext.css('visibility', 'hidden');
                    ppyPrev.css('visibility', 'hidden');
                }

                // display first image
                display(cur);

                // hide caption
                hideCaption();

                // get dims and position of newly created popeye box
                // after user CSS has been rendered by browser and thus
                // can be measured
                //--------------------------------------------------------------                
                var imHeight = ppyStageWrap.outerHeight();
                var imWidth = ppyStageWrap.outerWidth();

                var strMarginTop = obj.css('marginTop');
                if (strMarginTop == 'auto') {
                    strMarginTop = '0';
                }
                var strMarginLeft = obj.css('marginLeft');
                if (strMarginLeft == 'auto') {
                    strMarginLeft = '0';
                }

                var imTop = obj.offset().top - parseInt(strMarginTop, 10);
                var imLeft = obj.offset().left - parseInt(strMarginLeft, 10);
                var imRight = jQuery(window).width() - (obj.offset().left + imWidth) - parseInt(obj.css('marginRight'), 10);

                var imFloat = obj.css('float');

                var imMarginTop = obj.css('margin-top');
                var imMarginRight = obj.css('margin-right');
                var imMarginBottom = obj.css('margin-bottom');
                var imMarginLeft = obj.css('margin-left');

                // cut the popeye box out of the DOM and paste it to the bottom
                // in order to make it absolutely positioned
                // in it's place, insert a placeholder with the exact same dimensions
                //--------------------------------------------------------------
                // CSS: the placeholder dims and pos
                var cssPlaceholder = {
                    height: imHeight,
                    width: imWidth,
                    float: imFloat,
                    marginTop: imMarginTop,
                    marginRight: imMarginRight,
                    marginBottom: imMarginBottom,
                    marginLeft: imMarginLeft
                };

                // CSS: the new popeye box position
                var cssAbsolutePpy = {
                    position: 'absolute',
                    //top: imTop,
                    zIndex: '100'
                };
                if (imTop) {
                    cssAbsolutePpy.top = imTop;
                }

                // CSS: right or left orientation
                if (opts.direction == 'left' && imLeft) {
                    cssAbsolutePpy.left = imLeft;
                }
                else if (opts.direction == 'right' && imRight) {
                    cssAbsolutePpy.right = imRight;
                }

                // style and insert placeholder into DOM
                ppyPlaceholder.css(cssPlaceholder);
                obj.after(ppyPlaceholder);

                // move popeye box to bottom of DOM
                // and render it absolute
                obj.appendTo(jQuery('body'));
                obj.css(cssAbsolutePpy);


                // add event handlers
                //--------------------------------------------------------------

                // show tools on hover
                ppyStage.hover(
                    function(e) {
                        if (!opts.toolsAlwaysVisible)
                            ppyTools.stop().fadeTo(100, opts.opacity);
                    },
                    function(e) {
                        if (!opts.toolsAlwaysVisible)
                            ppyTools.stop().fadeTo(500, 0);
                    }
                );
                ppyTools.mouseleave(
                    function(e) {
                        if (!opts.toolsAlwaysVisible)
                            ppyTools.stop().fadeTo(500, 0);
                    }
                );
                ppyTools.mouseenter(
                    function(e) {
                        if (!opts.toolsAlwaysVisible)
                            ppyTools.stop().fadeTo(100, opts.opacity);
                    }
                );

                // show caption on hover
                obj.hover(
                    function(e) {
                        displayCaption(im.title[cur]);
                    },
                    function(e) {
                        hideCaption(true);
                    }
                );

                // previous image button
                ppyPrev.click(function() {
                    if (cur <= 0) {
                        cur = tot - 1;
                    } else {
                        cur--;
                    }
                    display(cur, true);
                    return false;
                });

                // next image button
                ppyNext.click(function() {
                    if (cur < (tot - 1)) {
                        cur++;
                    } else {
                        cur = 0;
                    }
                    display(cur, true);
                    return false;
                });

                // enlarge image button
                ppySwitch.click(function() {
                    enlarge(cur);
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
            var preloader = new Image();
            var preloader2 = new Image();
            var preloaderNext = new Image();
            var preloaderPrev = new Image();
            var preloaders = [];

            // html nodes
            var ppyPlaceholder = jQuery('<div class="ppy-placeholder" />');
            var ppyStageWrap = jQuery('<div class="ppy-stagewrap" />');
            var ppyStage = jQuery('<div class="ppy-stage" />');
            var ppyTools = jQuery('<div class="ppy-tools" />');
            var ppyPrev = jQuery('<div class="ppy-prev">' + opts.plabel + '</div>');
            var ppyNext = jQuery('<div class="ppy-next">' + opts.nlabel + '</div>');
            var ppySwitch = jQuery('<div class="ppy-enlarge">' + opts.blabel + '</div>');
            var ppyCap = jQuery('<div class="ppy-cap" />');
            var ppyCount = jQuery('<div class="ppy-count" />');
            var ppyCur = jQuery('<em class="ppy-cur" />');
            var ppyTotal = jQuery('<em class="ppy-total" />');
            var ppyTextWrap = jQuery('<div class="ppy-textwrap" />');
            var ppyText = jQuery('<span class="ppy-text" />');

            // declare image object arrays
            var im = {
                small: [],
                title: [],
                large: [],
                width: [],
                height: []
            };

            // start with bogus values, will be adjustet to real ones
            var maxWidth = 10000;
            var maxHeight = 10000;

            // counter vars
            var cur = 0;                        // array index of currently displayed image
            var tot = obj.find('img').length;   // total number of images
            var togo = tot;
            debug('jQuery.fn.popeye -> ' + tot + ' thumbnails found.');

            // declare CSS vars
            var cssCompactStage = {};
            var cssPpyTools = {};

            // popeye starts of in compact mode
            var enlarged = false;

            // preload all thumbs
            //--------------------------------------------------------------
            obj.find('li').each(function(i) {

                im.small[i] = jQuery(this).find('img').attr('src');   // the thumbnail url
                im.title[i] = jQuery(this).find('img').attr('alt');   // the image title
                im.large[i] = jQuery(this).find('a').attr('href');    // the image url
                debug('jQuery.fn.popeye -> Loading "' + im.small[i] + '"');

                // call init after all images have loaded
                jQuery(this).find('img').load(function() {

                    // if image that has loaded is last image in list
                    if (--togo < 1) {
                        debug('jQuery.fn.popeye -> All thumbnails loaded!');
                        init();
                    }
                }).attr('src', im.small[i]);
            });
        });
        
    };

    ////////////////////////////////////////////////////////////////////////////
    //
    // jQuery.fn.popeye.defaults
    // set default  options
    //
    ////////////////////////////////////////////////////////////////////////////
    jQuery.fn.popeye.defaults = {
        jsclass: 'ppy-js',           //class applied to popeye root element when JS is active
        nojsclass: 'ppy-no-js',        //class of optional element with warning to browsers without JS - element will be removed once popeye has loaded
        eclass: 'ppy-expanded',     //class to be applied to enlarged popeye-box
        lclass: 'ppy-loading',      //class to be applied to stage while loading image
        dlclass: 'ppy-left',         //class to be applied to ppy-tools if popeye opens to the left
        drclass: 'ppy-right',        //class to be applied to ppy-tools if popeye opens to the right
        direction: 'left',             //direction that popeye-box opens, can be "left" or "right"
        duration: 250,                //duration of transitional effect when enlarging or closing the box
        opacity: 0.7,                //opacity of navigational overlay
        toolsAlwaysVisible: false,
        countpos: 'overlay',          //position of image-counter - can be false, "overlay" or "caption"
        caption: true,               //display caption based on title attribute
        easing: 'swing',            //easing type, can be 'swing', 'linear' or any of jQuery Easing Plugin types (Plugin required)
        nlabel: '',                 //label for next button
        plabel: '',                 //label for previous button
        oflabel: 'of',               //label for image count text (e.g. 1 of 14)
        blabel: '',                 //label for enlarge button
        clabel: 'Click to close',   //label for expanded stage (to hint closing)
        debug: false               //turn on console output (slows down IE8!)

    };

    // end of closure, bind to jQuery Object
})(jQuery); 
