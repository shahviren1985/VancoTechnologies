jQuery(document).ready(function($){


/***************** Pretty Photo ******************/
	
	function prettyPhotoInit(){
		
		$("a.pp").prettyPhoto({
			theme: 'dark_rounded',
			allow_resize: true,
			default_width: 690,
			default_height: 388,
			social_tools: '',
			markup: '<div class="pp_pic_holder"> \
						   <div class="ppt">&nbsp;</div> \
							<div class="pp_details"> \
								<div class="pp_nav"> \
									<a href="#" class="pp_arrow_previous">Previous</a> \
									<p class="currentTextHolder">0/0</p> \
									<a href="#" class="pp_arrow_next">Next</a> \
								</div> \
								<a class="pp_close" href="#">Close</a> \
							</div> \
							<div class="pp_content_container"> \
								<div class="pp_left"> \
								<div class="pp_right"> \
									<div class="pp_content"> \
										<div class="pp_fade"> \
											<div class="pp_hoverContainer"> \
												<a class="pp_next" href="#">next</a> \
												<a class="pp_previous" href="#">previous</a> \
											</div> \
											<div id="pp_full_res"></div> \
										</div> \
									</div> \
								</div> \
								</div> \
							</div> \
						</div> \
						<div class="pp_loaderIcon"></div> \
						<div class="pp_overlay"></div>'
		});
		
	}
	
	prettyPhotoInit();
	

	
/***************** Smooth Scrolling ******************/

	function niceScrollInit(){
		$("html").niceScroll({
			scrollspeed: 60,
			mousescrollstep: 35,
			cursorwidth: 15,
			cursorborder: 0,
			cursorcolor: '#2D3032',
			cursorborderradius: 6,
			autohidemode: false,
			horizrailenabled: false
		});
		
		$('body, body #header-outer, body #header-secondary-outer, body #search-outer').css('padding-right','16px');
		
		$('html').addClass('no-overflow-y');
	}
	
	var $smoothActive = $('body').attr('data-smooth-scrolling'); 
	if( $smoothActive == 1 && $(window).width() > 690 && $('body').outerHeight(true) > $(window).height()){ 
    //niceScrollInit(); 
    }
	
	
	

/***************** Sliders ******************/

	var sliderAdvanceSpeed = parseInt($('#featured').attr('data-advance-speed'));
	var sliderAnimationSpeed = parseInt($('#featured').attr('data-animation-speed'));
	var sliderAutoplay = parseInt($('#featured').attr('data-autoplay'));
	
	if( isNaN(sliderAdvanceSpeed) ) { sliderAdvanceSpeed = 5500;}
	if( isNaN(sliderAnimationSpeed) ) { sliderAnimationSpeed = 800;}
	
	var $yPos;
	
	
	var img_urls=[];
	$('[style*="background"]').each(function() {
	    var style = $(this).attr('style');
	    var pattern = /background.*?url\('(.*?)'\)/g
	    var match = pattern.exec(style);
	    if (match) {        
	        img_urls.push(match[1]);
	    }
	});
	
	var imgArray = [];
	
	for(i=0;i<img_urls.length;i++){
		imgArray[i] = new Image();
		imgArray[i].src = img_urls[i];
	}
	 

	$(window).load(function(){
		
		//home slider
	
    	 
    	 $('#featured article .post-title h2 span').show();
    		 
    	////add hover effect to slider nav
    	$('.slider-nav > span').append('<span class="white"></span><span class="shadow"></span>');	
    	

    	////swipe for home slider
    	if($('body').hasClass('mobile')){
	    	$('#featured h2, #featured .video').swipe({
	    		swipeRight : function(e) {
					$('.slider-nav .left').trigger('click');
					e.stopImmediatePropagation();
					return false;
				 },
				 swipeLeft : function(e) {
					$('.slider-nav .right').trigger('click');
					e.stopImmediatePropagation();
					return false;
				 }    
	    	})
    	}
    	customSliderHeight();
		sliderAfterSetup();
	

	});
	
	//gallery
	$('.flex-gallery').each(function(){
		
		var $that = $(this);
		
		imagesLoaded($(this),function(instance){
		
			 $that.flexslider({
		        animation: 'fade',
		        controlsContainer: '.flexslider',
		        smoothHeight: false, 
		        animationSpeed: 500,
		        useCSS: false, 
		        touch: true
		    });
			
			////gallery slider add arrows
			$('.flex-gallery .flex-direction-nav li a.flex-next').html('<i class="icon-angle-right"></i>');
			$('.flex-gallery .flex-direction-nav li a.flex-prev').html('<i class="icon-angle-left"></i>');
		
		});
		
	});
	

	
	//home slider height
	var sliderHeight = parseInt($('#featured').attr('data-slider-height'));
	if( isNaN(sliderHeight) ) { sliderHeight = 650 } else { sliderHeight = sliderHeight -12 }; 
	
	////min height if video
	if( $('#featured .video').length > 0 && sliderHeight < 500) sliderHeight = 500;
	
	function customSliderHeight(){
		if(!$('body').hasClass('mobile')){
			$('#featured').attr('style', 'height: '+sliderHeight+'px !important');
			$('#featured article').css('height',sliderHeight+headerPadding2-23+'px')
		}
		else {
			$('#featured').attr('style', 'height: '+sliderHeight+'px');
		}
	}
	
	customSliderHeight();
	
	
	//home slider bg color
	if( $('#featured').length > 0 ){
		var sliderBackgroundColor = $('#featured').attr('data-bg-color');
		if( sliderBackgroundColor.length == 0 ) sliderBackgroundColor = '#000000'; 
		
		$('#featured article').css('background-color',sliderBackgroundColor);
	}
	
/***************** Parallax Slider ******************/
	
	//take into account header height when calculating the controls and info positioning 
	var logoHeight = parseInt($('#header-outer').attr('data-logo-height'));
	var headerPadding = parseInt($('#header-outer').attr('data-padding'));
	var headerPadding2 = parseInt($('#header-outer').attr('data-padding'));
	var extraDef = 10;
	var headerResize = ($('body').hasClass('pp-video-function')) ? '1' : $('#header-outer').attr('data-header-resize');
	var headerResizeOffExtra = 0;
	var extraHeight = ($('#wpadminbar').length > 0) ? 28 : 0; //admin bar
	var usingLogoImage = true;
    var mediaElement = ($('.wp-video-shortcode').length > 0) ? 36 : 0;
    var secondaryHeader = ($('#header-outer').attr('data-using-secondary') == '1') ? 33 : 0 ;
    
	if( isNaN(logoHeight) ) { usingLogoImage = false; logoHeight = 30;}
	if( isNaN(headerPadding) ) { headerPadding = 28; headerPadding2 = 28;}
	
    if( $('header#top #logo img').length == 0 ) { logoHeight = 30; }
    
	var $captionPos = (((sliderHeight-70)/2 - $('div.slider-nav span.left span.white').height()/2) + headerPadding2 - headerResizeOffExtra) - 75;
	var $controlsPos = (((sliderHeight-70)/2 - $('div.slider-nav span.left span.white').height()/2) + logoHeight + headerPadding*2 + extraHeight + secondaryHeader) -10;
	
	var $scrollTop;
	var $videoHeight; 
	
	//inital load
	function sliderAfterSetup(){
		//webkit video fix
		$('#featured .mejs-container').css('width',$('#featured .video').width());
		$('#featured .mejs-container').css('height',$('#featured .video').width()/1.7777);
		$(window).trigger('resize');
		
		$('body:not(.mobile) .orbit-wrapper #featured .orbit-slide:not(".has-video") article .container').css('top', $captionPos +"px");
		$('body:not(.mobile) .orbit-wrapper #featured .orbit-slide.has-video article .container').css('top', $videoHeight +"px");
		$('body:not(.mobile) .orbit-wrapper .slider-nav > span').css('top', $controlsPos +"px");	
		$('body:not(.mobile) .orbit-wrapper #featured .slide article').css({'top': ((- $scrollTop / 5)+logoHeight+headerPadding2+headerResizeOffExtra+extraHeight-extraDef+secondaryHeader)  + 'px' });
		
		//height fix for when resize on scroll if off
	
	}
	
	
	function videoSlidePos(){
		$('#featured > div').has('.video').each(function(){
			if( $(window).width() > 1300 ) {
				$('#featured .orbit-slide.has-video .video, #featured .orbit-slide.has-video h2').css('top','0');
				$('#featured .orbit-slide.has-video .post-title > a').css('top','10px');

				$videoHeight = ((sliderHeight-28)/2) - ((410-mediaElement)/2) + headerPadding2 - headerResizeOffExtra;
			}
			
			else if( $(window).width() > 1000 && $(window).width() < 1081 ){
				$('#featured .orbit-slide.has-video .video, #featured .orbit-slide.has-video h2').css('top','0');
				$('#featured .orbit-slide.has-video .post-title > a').css('top','10px');
				
				$videoHeight = ((sliderHeight-28)/2) - ((290-mediaElement)/2) + headerPadding2 - headerResizeOffExtra;
			}
			
			else {
				$videoHeight = ((sliderHeight-28)/2) - ((336-mediaElement)/2) +headerPadding2 - headerResizeOffExtra;
			}
	
		});
	}
	
	videoSlidePos();
	
	//dynamic controls and info positioning
	function controlsAndInfoPos(){
		$scrollTop = $(window).scrollTop();
		
		$('body:not(.mobile) .orbit-wrapper #featured .orbit-slide:not(".has-video") article .container').css({ 
			'opacity' : 1-($scrollTop/(sliderHeight-130)),
			'top' : ($scrollTop*-0.2) + $captionPos +"px"
		});
		
		//video slides
		$('body:not(.mobile) .orbit-wrapper #featured .orbit-slide.has-video article .container').css({ 
			'opacity' : 1-($scrollTop/(sliderHeight-130)),
			'top' : ($scrollTop*-0.2) + $videoHeight +"px"
		});
		
		if($('#boxed').length == 0){
			$('body:not(.mobile) .orbit-wrapper .slider-nav > span').css({ 
				'opacity' : 1-($scrollTop/(sliderHeight-130)),
				'top' : ($scrollTop*-0.4) + $controlsPos +"px"
			});
		}
		
	}
	
	controlsInit();
	function controlsInit(){

		if($('#boxed').length > 0) {
			if(1-$scrollTop/(sliderHeight-$controlsPos-20) >= 0){
				$(window).unbind('scroll',hideControls);
				$(window).bind('scroll',showControls);
			} else {
				$(window).unbind('scroll',showControls);
				$(window).bind('scroll',hideControls);
			}
		} else {
			$(window).unbind('scroll',showControls);
			$(window).unbind('scroll',hideControls);
		}
	
	}
	
	function showControls(){

		if(1-$scrollTop/(sliderHeight-$controlsPos-20) >= 0){
			$('body:not(.mobile) .orbit-wrapper .slider-nav > span.left').stop(true,true).animate({ 'left' : '0px'},450,'easeOutCubic');
			if($('body').attr('data-smooth-scrolling')=='1'){
				$('body:not(.mobile) .orbit-wrapper .slider-nav > span.right').stop(true,true).animate({ 'right' : '15px'},450,'easeOutCubic');
			} else {
				$('body:not(.mobile) .orbit-wrapper .slider-nav > span.right').stop(true,true).animate({ 'right' : '0px'},450,'easeOutCubic');
			}
			$(window).unbind('scroll',showControls);
			$(window).bind('scroll',hideControls);
		}
	}
	
	function hideControls(){
		
		if(1-$scrollTop/(sliderHeight-$controlsPos-20) < 0){
			$('body:not(.mobile) .orbit-wrapper .slider-nav > span.left').stop(true,true).animate({ 'left' : '-80px'},450,'easeOutCubic');
			$('body:not(.mobile) .orbit-wrapper .slider-nav > span.right').stop(true,true).animate({ 'right' : '-80px'},450,'easeOutCubic');
			$(window).unbind('scroll',hideControls);
			$(window).bind('scroll',showControls);
		}
		
	}
	
	if( $('#featured').length > 0){
	
		$(window).scroll(function(){
		    
		    //hide video to not mess up parallax section
		    $('#featured .mejs-mediaelement, #featured .iframe-embed').each(function(){
		    
		    	if( $(this).parents('.container').css('opacity') <= 0){
			    	$(this).css('visibility','hidden').hide();
			    } else {
			    	$(this).css('visibility','visible').show();
			    }
		    });
		    
		    if(!$('body').hasClass('mobile')){
		    	
		    	controlsAndInfoPos();
				$('body:not(.mobile) .orbit-wrapper #featured .slide:not(:transparent) article').css({'top': ((- $scrollTop / 5)+logoHeight+headerPadding2+headerResizeOffExtra+extraHeight-extraDef+secondaryHeader)  + 'px' });	
			
			}
		});
		
		//disable parallax for mobile
		$(window).resize(function(){
			if(!$('body').hasClass('mobile')){
				$('.orbit-wrapper #featured article').css('top', ((- $scrollTop / 5)+logoHeight+headerPadding2+headerResizeOffExtra+extraHeight-extraDef+secondaryHeader)  + 'px');
				$('.orbit-wrapper div.slider-nav span.right, .orbit-wrapper div.slider-nav span.left').html('<span class="white"></span><span class="shadow"></span>');
			} else {
				$('.orbit-wrapper div.slider-nav span.right').html('<i class="icon-angle-right"></i>');
				$('.orbit-wrapper div.slider-nav span.left').html('<i class="icon-angle-left"></i>');
			}

			videoSlidePos();
			controlsAndInfoPos();
			customSliderHeight();
			
			//height fix for when resize on scroll if off
			if(!$('body').hasClass('mobile') && headerResize.length == 0){
				$('#featured article').css('height',sliderHeight-32+'px')
			}
			
		});
		
	}

    
    //webkit self-hosted video fix
    $('.jp-video-container .jp-play, jp-video-container .jp-seek-bar').click(function(){
    	$(this).parents('.jp-video-container').prev('.jp-jplayer').find('video').show().css('display','block');
    	$(this).parents('.jp-video-container').prev('.jp-jplayer').find('.jp-jplayer > img').hide();
    });
    
    //mobile video more info
    $('#featured .span_12 a.more-info').click(function(){
    	if( !$(this).find('.btv').is(":visible")){
    		$(this).parent().parent().find('h2, > a').css('opacity',1);
    		$(this).parent().parent().find('.video').stop().animate({'top':'-400px'},800,'easeOutCubic');
	    	$(this).parent().parent().find('h2').stop().animate({'top':'-400px'},800,'easeOutCubic');
	    	$(this).parent().parent().find('> a').stop().animate({'top':'-380px'},800,'easeOutCubic');
	    	$(this).find('.btv').show();
	    	$(this).find('.mi').hide();
    	}
    	else {
    		$(this).parent().parent().find('.video').stop().animate({'top':'0px'},800,'easeOutCubic');
	    	$(this).parent().parent().find('h2').stop().animate({'top':'0px'},800,'easeOutCubic');
	    	$(this).parent().parent().find('> a').stop().animate({'top':'0px'},800,'easeOutCubic');
	    	$(this).find('.mi').show();
	    	$(this).find('.btv').hide();
    	}
    	
    	return false;
    });

/***************** Superfish ******************/

	

	//turn dropdown arrows into font awesome
	$('nav > ul.sf-menu > li').each(function(){
		$(this).find(' > a.sf-with-ul').append('<span class="sf-sub-indicator"><i class="icon-angle-down"></i></span>');
	});
	
	function SFArrows(){

		$('nav > ul.sf-menu > li > ul li').each(function(){ 
			$(this).find(' > a.sf-with-ul').append('<span class="sf-sub-indicator"><i class="icon-angle-right"></i></span>');
		});	
	
		//set height of dropdown arrow
		$('.sf-sub-indicator').css('height',$('a.sf-with-ul').height());
	}
	
	SFArrows();
	

	


/***************** Caroufredsel ******************/

    
    	
	$('.carousel:not(".clients")').each(function(){
    	var $that = $(this); 
    	var scrollSpeed, easing;
				
		(parseInt($(this).attr('data-scroll-speed'))) ? scrollSpeed = parseInt($(this).attr('data-scroll-speed')) : scrollSpeed = 700;
		($(this).attr('data-easing').length > 0) ? easing = $(this).attr('data-easing') : easing = 'linear';
		
		
		var $element = $that;
		if($that.find('img').length == 0) $element = $('body');
		
		imagesLoaded($element,function(instance){

			
	    	$that.carouFredSel({
	    		circular: true,
	    		responsive: true,
		        items       : {
					width : 353,
			        visible     : {
			            min         : 1,
			            max         : 3
			        }
			    },
			    swipe       : {
			        onTouch     : true,
			        options      : {
			        	excludedElements: "button, input, select, textarea, .noSwipe"	
			        }
			    },
			    scroll: {
			    	easing          : easing,
		            duration        : scrollSpeed
			    },
		        prev    : {
			        button  : function() {
			           return $that.parents('.carousel-wrap').prev(".carousel-heading").find('.carousel-prev');
			        }
		    	},
			    next    : {
		       		button  : function() {
			           return $that.parents('.carousel-wrap').prev(".carousel-heading").find('.carousel-next');
			        }
			    },
			    auto    : {
			    	play: false
			    }
		    }).animate({'opacity': 1},1300);
		    
		    $that.parents('.carousel-wrap').wrap('<div class="carousel-outer">');
		    
	     });//images loaded
	     
    });//each
    
    $(window).load(piVertCenter);


     $('.carousel.clients').each(function(){
    	var $that = $(this);
    	var columns; 
    	(parseInt($(this).attr('data-max'))) ? columns = parseInt($(this).attr('data-max')) : columns = 5;
    	if($(window).width() < 690 && $('body').attr('data-responsive') == '1') { columns = 2; $(this).addClass('phone') }
    	
    	var $element = $that;
		if($that.find('img').length == 0) $element = $('body');
		
		imagesLoaded($element,function(instance){
    		
	    	$that.carouFredSel({
		    		circular: true,
		    		responsive: true, 
			        items       : {
						
						height : $that.find('> div:first').height(),
						width  : $that.find('> div:first').width(),
				        visible     : {
				            min         : 1,
				            max         : columns
				        }
				    },
				    swipe       : {
				        onTouch     : true
				    },
				    scroll: {
				    	items           : 1,
				    	easing          : 'easeInOutCubic',
			            duration        : '800',
			            pauseOnHover    : true
				    },
				    auto    : {
				    	play            : true,
				    	timeoutDuration : 2700
				    }
		    }).animate({'opacity': 1},1300);
		    
		    $that.parents('.carousel-wrap').wrap('<div class="carousel-outer">');
		     
		    //cients carousel height
	  		$(window).resize(function(){
	  			
	  			var tallestImage = 0;
	  			
		    	 $('.carousel.clients').each(function(){
		    	 	
		    	 	$(this).find('> div').each(function(){
						($(this).height() > tallestImage) ? tallestImage = $(this).height() : tallestImage = tallestImage;
					});	
		    	 	
		         	$(this).css('height',tallestImage);
		         	$(this).parent().css('height',tallestImage);
		         });
	   	    });  	
		    
		    $(window).trigger('resize');
		    
	    
	    });

    });
    
    
    
	    
	    


    
    var resizeTimer = null;
    $(window).resize(function() {
        resizeTimer && clearTimeout(resizeTimer); 
        resizeTimer = setTimeout(function() {
          		
             piVertCenter();
                  
        }, 60);
    });
	
	
	//fadein for clients / carousels
	$('.clients.fade-in-animation').each(function() {

		$(this).appear(function() {
			$(this).find('> div').each(function(i){
				$(this).delay(i*90).animate({'opacity':"1"},400);
			});
			
			var $that = $(this);
			
			//add the css transition class back in after the aniamtion is done
			setTimeout(function(){ $that.addClass('completed'); },($(this).find('> div').length*90) + 400);
		},{accX: 0, accY: -155},'easeInCubic');
	
	}); 
	
	
/*-------------------------------------------------------------------------*/
/*	2.	Helper Functions
/*-------------------------------------------------------------------------*/

	jQuery.fn.setCursorPosition = function(position){
	    if(this.lengh == 0) return this;
	    return $(this).setSelection(position, position);
	}
	
	jQuery.fn.setSelection = function(selectionStart, selectionEnd) {
	    if(this.lengh == 0) return this;
	    input = this[0];
	
	    if (input.createTextRange) {
	        var range = input.createTextRange();
	        range.collapse(true);
	        range.moveEnd('character', selectionEnd);
	        range.moveStart('character', selectionStart);
	        range.select();
	    } else if (input.setSelectionRange) {
	        input.focus();
	        input.setSelectionRange(selectionStart, selectionEnd);
	    }
	
	    return this;
	}
	
	
	$.belowthefold = function(element, settings) {
        var fold = $(window).height() + $(window).scrollTop();
        return fold <= $(element).offset().top - settings.threshold;
    };

    $.abovethetop = function(element, settings) {
        var top = $(window).scrollTop();
        return top >= $(element).offset().top + $(element).height() - settings.threshold;
    };
    
    $.rightofscreen = function(element, settings) {
        var fold = $(window).width() + $(window).scrollLeft();
        return fold <= $(element).offset().left - settings.threshold;
    };
    
    $.leftofscreen = function(element, settings) {
        var left = $(window).scrollLeft();
        return left >= $(element).offset().left + $(element).width() - settings.threshold;
    };
    
    $.inviewport = function(element, settings) {
        return !$.rightofscreen(element, settings) && !$.leftofscreen(element, settings) && !$.belowthefold(element, settings) && !$.abovethetop(element, settings);
    };
    
    $.extend($.expr[':'], {
        "below-the-fold": function(a, i, m) {
            return $.belowthefold(a, {threshold : 0});
        },
        "above-the-top": function(a, i, m) {
            return $.abovethetop(a, {threshold : 0});
        },
        "left-of-screen": function(a, i, m) {
            return $.leftofscreen(a, {threshold : 0});
        },
        "right-of-screen": function(a, i, m) {
            return $.rightofscreen(a, {threshold : 0});
        },
        "in-viewport": function(a, i, m) {
            return $.inviewport(a, {threshold : 0});
        }
    });
	
	
	$.extend($.expr[':'], {
	    transparent: function(elem, i, attr){
	      return( $(elem).css("opacity") === "0" );
	    }
	});

/*-------------------------------------------------------------------------*/
/*	3.	Shortcode Stuff
/*-------------------------------------------------------------------------*/

/***************** Tabbed ******************/

	$('.tabbed ul li a').click(function(){
		var $id = $(this).attr('href');
		
		if(!$(this).hasClass('active-tab')){
			$('.tabbed ul li a').removeClass('active-tab');
			$(this).addClass('active-tab');
			
			$('.tabbed > div:not(.clear)').css({'visibility':'hidden','position':'absolute','opacity':'0','left':'-9999px','display':'none'});
			$('.tabbed > div'+$id).css({'visibility':'visible', 'position' : 'relative','left':'0','display':'block'}).stop().animate({'opacity':1});
			
		}
		
		return false;
	});
	
	$('.tabbed ul li:first-child a').click();
	
	
/***************** Toggle ******************/
	
	//toggles
	$('.toggle h3 a').click(function(){
		
		if(!$(this).parents('.toggles').hasClass('accordion')) { 
			$(this).parents('.toggle').find('> div').slideToggle(300);
			$(this).parents('.toggle').toggleClass('open');
			
			//switch icon
			if( $(this).parents('.toggle').hasClass('open') ){
				$(this).find('i').attr('class','icon-minus-sign');
			} else {
				$(this).find('i').attr('class','icon-plus-sign');
			}
			
			return false;
		}
	});
	
	//accordion
	$('.accordion .toggle h3 a').click(function(){
		
		if($(this).parents('.toggle').hasClass('open')) return false;
		
		$(this).parents('.toggles').find('.toggle > div').slideUp(300);
		$(this).parents('.toggles').find('.toggle h3 a i').attr('class','icon-plus-sign');
		$(this).parents('.toggles').find('.toggle').removeClass('open');
		
		$(this).parents('.toggle').find('> div').slideDown(300);
		$(this).parents('.toggle').addClass('open');
		
		//switch icon
		if( $(this).parents('.toggle').hasClass('open') ){
			$(this).find('i').attr('class','icon-minus-sign');
		} else {
			$(this).find('i').attr('class','icon-plus-sign');
		}
		
		return false;
	});
	
	//accordion start open
	$('.accordion > .toggle').first().addClass('open').find('> div').show();
	$('.accordion > .toggle').first().find('i').attr('class','icon-minus-sign');
	

/***************** PARALLAX SECTIONS ******************/


	var $window = $(window);
	var windowHeight = $window.height();
	
	$window.resize(function () {
		windowHeight = $window.height();
	});
	
	$.fn.parallax = function(xpos, speedFactor, outerHeight) {
		var $this = $(this);
		var getHeight;
		var firstTop;
		var paddingTop = 0;
		
		//get the starting position of each element to have parallax applied to it		
		$this.each(function(){
		    firstTop = $this.offset().top;
		});
		
		$window.resize(function () {
			$this.each(function(){
		  	    firstTop = $this.offset().top;
			});
		});
		
		$window.load(function(){
			$this.each(function(){
		  	    firstTop = $this.offset().top;
			}); 
		});
	 
	
		getHeight = function(jqo) {
			return jqo.outerHeight(true);
		};
		 
			
		// setup defaults if arguments aren't specified
		if (arguments.length < 1 || xpos === null) xpos = "50%";
		if (arguments.length < 2 || speedFactor === null) speedFactor = 0.1;
		if (arguments.length < 3 || outerHeight === null) outerHeight = true;
		
		// function to be called whenever the window is scrolled or resized
		function update(){
			var pos = $window.scrollTop();				
	
			$this.each(function(){
				var $element = $(this);
				var top = $element.offset().top;
				var height = getHeight($element);
				// Check if totally above or totally below viewport
				if (top + height < pos || top > pos + windowHeight) {
					return;
				}
	
				$this.css('backgroundPosition', xpos + " " + Math.round((firstTop - pos) * speedFactor) + "px");
			});
		}		
	
		$window.bind('scroll', update).resize(update);
		update();
	};



	
/***************** Full Width Section ******************/
	function fullWidthSections(){
		
		if($('#boxed').length == 1){
			$justOutOfSight = (((parseInt($('.container').css('max-width'))+102) - parseInt($('.container').css('max-width'))) / 2) 
		} else {
			$justOutOfSight = (($(window).width() - parseInt($('.container').css('max-width'))) / 2) 
		}
		
		$('.full-width-section').each(function(){
			
			if(!$(this).parent().hasClass('span_9') && !$(this).parent().hasClass('span_3') && $(this).parent().attr('id') != 'sidebar-inner' && $(this).parent().attr('id') != 'portfolio-extra'){
				$(this).css({
					'margin-left': - $justOutOfSight,
					'padding-left': $justOutOfSight,
					'padding-right': $justOutOfSight,
					'visibility': 'visible'
				});	
			} else {
				$(this).css({
					'margin-left': 0,
					'padding-left': 0,
					'padding-right': 0,
					'visibility': 'visible'
				});	
			}
			
		});
	}
	
	var $contentElementsNum = $('.main-content > .row > *').length;

	$('.full-width-section.parallax_section').each(function(){
	   var $id = $(this).attr('id');
	   $('#'+$id + ".parallax_section").parallax("49%", 0.2);
	});
	
	
	//if fullwidth section is first or last, remove the margins so it fits flush against header/footer
	$('.full-width-section').each(function(){
		if(!$(this).parent().hasClass('span_9') && !$(this).parent().hasClass('span_3') && $(this).parent().attr('id') != 'sidebar-inner' && $(this).parent().attr('id') != 'portfolio-extra'){
			////first
			if($(this).index() == '0' && $('#page-header-bg').length == 0){
				
				$(this).css('margin-top','-53px').addClass('first-section');
				
				//check if it's also last (i.e. the only fws)
				if($(this).index() == $contentElementsNum-1) {
					$(this).css('margin-bottom','-40px');
					$('#call-to-action .triangle').remove();
				}
				
			} 
			else if($(this).index() == '0' && $('#page-header-bg').length == 1){
				$(this).css('margin-top','-2.1em').addClass('first-section under-page-header');
			} 
			//last
			else if($(this).index() == $contentElementsNum-1) {
				$(this).css('margin-bottom','-40px');
				$('#call-to-action .triangle').remove();
			}
		}
	});
	
	
	//recalc size on reszie
	fullWidthSections();
	$(window).resize(fullWidthSections);
	
	
	
/***************** Checkmarks ******************/

	$('ul.checks li').prepend('<i class="icon-ok-sign"></i>');
	

/***************** Image with Animation *******************/

$('img.img-with-animation').each(function() {

	$(this).appear(function() {
		if($(this).attr('data-animation') == 'fade-in-from-left'){
			$(this).animate({
				'opacity' : 1,
				'left' : '0px'
			},600);
		} else if($(this).attr('data-animation') == 'fade-in-from-right'){
			$(this).animate({
				'opacity' : 1,
				'right' : '0px'
			},600);
		} else {
			$(this).animate({
			'opacity' : 1
		},600);
		}
	},{accX: 0, accY: -155},'easeInCubic');

}); 	
	
/***************** 4 Col Grid in iPad ******************/

	$('.col.span_3').each(function(){
		var $currentDiv = $(this);
		var $nextDiv = $(this).next('div');
		if( $nextDiv.hasClass('span_3') && !$currentDiv.hasClass('one-fourths')) {
			$currentDiv.addClass('one-fourths clear-both');
			$nextDiv.addClass('one-fourths right-edge');
		}
	});
	
	
/***************** Bar Graph ******************/
	
	$('.bar_graph li').each(function(i){
		
		$(this).appear(function(){
			
			var percent = $(this).find('span').attr('data-width');
			
			$(this).find('span').animate({
				'width' : percent + '%'
			},1700, 'easeOutCirc',function(){
			});
			
			$(this).find('span strong').animate({
				'opacity' : 1
			},1400);	
			
			////100% progress bar 
			if(percent == '100'){
				$(this).find('span strong').addClass('full');
			}
			
		});

	});
	



/***************** Pricing Tables ******************/


var $tallestCol;

function pricingTableHeight(){
	$('.pricing-table').each(function(){
		$tallestCol = 0;
		
		$(this).find('> div .features').each(function(){
			($(this).height() > $tallestCol) ? $tallestCol = $(this).height() : $tallestCol = $tallestCol;
		});	
		
		//safety net incase pricing tables height couldn't be determined
		if($tallestCol == 0) $tallestCol = 'auto';
		
		//set even height
		$(this).find('> div .features').css('height',$tallestCol);

	});
}

pricingTableHeight();


/***************** Testimonial Slider ******************/

//testimonial slider controls
$('body').on('click','.testimonial_slider .controls li', function(){
	var $index = $(this).index();
	
	$(this).parents('.testimonial_slider').find('li').removeClass('active-testimonial').html('<i class="icon-circle"></i>');
	$(this).html('<i class="icon-circle-blank"></i>').addClass('active-testimonial');
	
	$(this).parents('.testimonial_slider').find('.slides blockquote').stop().css({'opacity':'0', 'left':'-15px', 'z-index': '1'});
	$(this).parents('.testimonial_slider').find('.slides blockquote').eq($index).stop(true,true).animate({'opacity':'1','left':'0'},500,'easeOutCubic').css('z-index','20');
});


var $tallestQuote;

//create controls
$('.testimonial_slider').each(function(){
	
	$(this).append('<div class="controls"><ul></ul></div>');
	
	var slideNum = $(this).find('blockquote').length;
	var $that = $(this);
	
	for(var i=0;i<slideNum;i++) {
		$that.find('.controls ul').append('<li><i class="icon-circle"></i></li>')
	}
	
	//activate first slide
	$(this).find('.controls ul li').first().click();
	
});
	
function testimonialSliderHeight() {
		
	$('.testimonial_slider').each(function(){
		$tallestQuote = 0;
			
		$(this).find('blockquote').each(function(){
			($(this).height() > $tallestQuote) ? $tallestQuote = $(this).height() : $tallestQuote = $tallestQuote;
		});	
		
		//safety net incase pricing tables height couldn't be determined
		if($tallestQuote == 0) $tallestQuote = 100;
		
		//set even height
		$(this).css('height',$tallestQuote+20+'px');
		
		//show the slider once height is set
		$(this).animate({'opacity':'1'});

	});

}

testimonialSliderHeight();
$(window).resize(testimonialSliderHeight);

////swipe for testimonials
if($('body').hasClass('mobile')){
	$('.testimonial_slider').swipe({

		swipeLeft : function(e) {
			$(this).find('.controls ul li.active-testimonial').next('li').trigger('click');
			e.stopImmediatePropagation();
			return false;
		 },
		 swipeRight : function(e) {
			$(this).find('.controls ul li.active-testimonial').prev('li').trigger('click');
			e.stopImmediatePropagation();
			return false;
		 }    
	})
}


/***************** WP Media Embed / External Embed ******************/

//this isn't the for the video shortcode* This is to help any external iframe embed fit & resize correctly 
$('iframe').each(function(){
	
	//make sure the iframe has a src (things like adsense don't)
	if(typeof $(this).attr('src') != 'undefined' ){
		
		if( $(this).attr('src').toLowerCase().indexOf("youtube") >= 0 || $(this).attr('src').toLowerCase().indexOf("vimeo") >= 0 ) {
			$(this).wrap('<div class="iframe-embed"/>');	
			
			//add ?wmode=transparent to all youytube embeds to fix z-index issues in IE
			if($(this).attr('src').indexOf('?wmode=transparent') == -1) {
				$(this).attr('src',$(this).attr('src') + '?wmode=transparent');
			}
		}
		 
	} else {
		$(this).wrap('<div class="iframe-embed-standard"/>');
	}
	
})


//unwrap post and protfolio videos
$('.video-wrap iframe, #sidebar iframe').unwrap();


$('video').attr('width','100%');
$('video').attr('height','100%'); 

$('audio').attr('width','100%');
$('audio').attr('height','100%');

$('audio').css('visibility','visible');

if($('body').hasClass('mobile')){
	$('video').css('visibility','hidden');
} else {
	$('video').css('visibility','visible');
}

$(window).load(function(){
	$('video').css('visibility','visible');
	$(window).trigger('resize');
	setTimeout(function(){$(window).trigger('resize');},200);
});

$(window).trigger('resize');

//webkit video back button fix 
$('iframe').each(function(){
	$(this).attr('src',$(this).attr('src'));
	$(this).css({'opacity':'1', 'visibility':'visible'});
});

/*-------------------------------------------------------------------------*/
/*	4.	Header + Search
/*-------------------------------------------------------------------------*/	 



/***************** Page Headers ******************/

var pageHeaderHeight = parseInt($('#page-header-bg').attr('data-height'));
var pageHeadingHeight;

//set the user defined height
$('#page-header-bg').css('height',pageHeaderHeight+'px');
	
function pageHeader(){
	
	if(!$('body').hasClass('mobile')){
		
		//recalc
		pageHeaderHeight = parseInt($('#page-header-bg').attr('data-height'));
		$('#page-header-bg .container > .row').css('top',0);
		
		//center the heading
		pageHeadingHeight = $('#page-header-bg .col.span_6').height();
		$('#page-header-bg:not("[data-parallax=1]") .col.span_6').css('top', (pageHeaderHeight/2) - (pageHeadingHeight/2) + 18);
		
		//center portfolio filters
		$('#page-header-bg:not("[data-parallax=1]") #portfolio-filters').css('top', (pageHeaderHeight/2) + 2);	
	}
	
	else {
		//recalc
		pageHeaderHeight = parseInt($('#page-header-bg').attr('data-height'));
		
		//center the heading
		var pageHeadingHeight = $('#page-header-bg .container > .row').height();
		$('#page-header-bg .container > .row').css('top', (pageHeaderHeight/2) - (pageHeadingHeight/2) + 12);
		
	}
	
	$('#page-header-bg .container > .row').css('visibility','visible');
}

pageHeader();
$(window).resize(pageHeader);

if($('#header-outer').attr('data-header-resize') == '' || $('#header-outer').attr('data-header-resize') == '0'){
	$('#page-header-wrap').css('margin-top','0');
}


/***************** Parallax Page Headers ******************/
if($('#page-header-bg[data-parallax="1"]').length > 0) {

	//fadeIn
	function extractUrl(input) {
 		return input.replace(/"/g,"").replace(/url\(|\)$/ig, "");
	}
	
	var img = new Image();
	
	var imgX, imgY, aspectRatio;
	var diffX, diffY;
	var pageHeadingHeight = $('#page-header-bg .col.span_6').height();
	var pageHeaderHeight = parseInt($('#page-header-bg').attr('data-height'));
	
	img.onload = function() {
	
	  $('#page-header-bg[data-parallax="1"]').css({'top': (logoHeight+headerPadding2+headerResizeOffExtra+extraHeight-extraDef+secondaryHeader)  + 'px' });
	  $('#page-header-bg[data-parallax="1"]').animate({ 'opacity' : 1},500);
	  
	  $('#page-header-wrap').css({'height' : pageHeaderHeight});
	  
	  $('#page-header-bg[data-parallax="1"] .span_6').css({ 
			'opacity' : 1-($scrollTop/(pageHeaderHeight-($('#page-header-bg .col.span_6').height()*2)+75)),
			'top' : ($scrollTop*-0.45) + (logoHeight+headerPadding2+headerResizeOffExtra+extraHeight-extraDef+secondaryHeader) + ((pageHeaderHeight/2) - (pageHeadingHeight/2))  +"px"
	   });
	   
	   $('#page-header-bg[data-parallax="1"] #portfolio-filters').css({ 
			'opacity' : 1-($scrollTop/(pageHeaderHeight-($('#page-header-bg .col.span_6').height()*2)+75)),
			'top' : ($scrollTop*-0.10) + ((pageHeaderHeight/2) - 7)  +"px"
	   });
	}
	
	img.src = extractUrl($('#page-header-bg[data-parallax="1"]').css('background-image'));
	
	
	
	
	$(window).scroll(function(){
		
		var $scrollTop = $(window).scrollTop();
		
		if(!$('body').hasClass('mobile')){
	
			//calc bg pos
			 $('#page-header-bg[data-parallax="1"]').css({'top': ((- $scrollTop / 5)+logoHeight+headerPadding2+headerResizeOffExtra+extraHeight-extraDef+secondaryHeader)  + 'px' });
			
			
			$('#page-header-bg[data-parallax="1"] .span_6').css({ 
				'opacity' : 1-($scrollTop/(pageHeaderHeight-($('#page-header-bg .col.span_6').height()*2)+75)),
				'top' : ($scrollTop*-0.45) + (logoHeight+headerPadding2+headerResizeOffExtra+extraHeight-extraDef+secondaryHeader) + ((pageHeaderHeight/2) - (pageHeadingHeight/2)) +"px"
			});
			
			$('#page-header-bg[data-parallax="1"] #portfolio-filters').css({ 
				'opacity' : 1-($scrollTop/(pageHeaderHeight-($('#page-header-bg .col.span_6').height()*2)-25)),
				'top' : ($scrollTop*-0.10) + ((pageHeaderHeight/2) - 7)  +"px"
		   });
			
			if($('#page-header-bg[data-parallax="1"] .span_6').css('opacity') == 0){
				$('#page-header-bg[data-parallax="1"] .span_6, #page-header-bg[data-parallax="1"] #portfolio-filters').hide();
			} else {
				$('#page-header-bg[data-parallax="1"] .span_6, #page-header-bg[data-parallax="1"] #portfolio-filters').show();
			}
			
		}
		
	
	});
}





/***************** WooCommerce Cart *****************/
var timeout;
var productToAdd;

//notification
$('.woocommerce .product-wrap .add_to_cart_button').click(function(){
	productToAdd = $(this).parents('li').find('h3').text();
	$('#header-outer .cart-notification span.item-name').html(productToAdd);
});

//notification hover
$('#header-outer .cart-notification').hover(function(){
	$(this).fadeOut(400);
	$('#header-outer .widget_shopping_cart').stop(true,true).fadeIn(400);
	$('#header-outer .cart_list').stop(true,true).fadeIn(400);
	clearTimeout(timeout);
});

//cart dropdown
$('#header-outer div.cart-outer').hover(function(){
	$('#header-outer .widget_shopping_cart').stop(true,true).fadeIn(400);
	$('#header-outer .cart_list').stop(true,true).fadeIn(400);
	clearTimeout(timeout);
	$('#header-outer .cart-notification').fadeOut(300);
},function(e){
	$('#header-outer .widget_shopping_cart').stop(true,true).fadeOut(300);
	$('#header-outer .cart_list').stop(true,true).fadeOut(300);
});

$('body').bind('added_to_cart', shopping_cart_dropdown_show);
$('body').bind('added_to_cart', shopping_cart_dropdown);

function shopping_cart_dropdown() {
		
		if(!$('.widget_shopping_cart .widget_shopping_cart_content .cart_list .empty').length && $('.widget_shopping_cart .widget_shopping_cart_content .cart_list').length > 0 ) {
			$('.cart-menu-wrap').addClass('has_products');
		}
}

function shopping_cart_dropdown_show(e) {
		
		clearTimeout(timeout);
		
		if(!$('.widget_shopping_cart .widget_shopping_cart_content .cart_list .empty').length && $('.widget_shopping_cart .widget_shopping_cart_content .cart_list').length > 0 && typeof e.type != 'undefined' ) {
			//before cart has slide in
			if(!$('#header-outer .cart-menu-wrap').hasClass('has_products')) {
				setTimeout(function(){ $('#header-outer .cart-notification').fadeIn(400); },400);
			}
			else if(!$('#header-outer .cart-notification').is(':visible')) {
				$('#header-outer .cart-notification').fadeIn(400);
			} else {
				$('#header-outer .cart-notification').show();
			}
			timeout = setTimeout(hideCart,2700);
		}
}

function hideCart() {
	$('#header-outer .cart-notification').stop(true,true).fadeOut();
}

setTimeout(shopping_cart_dropdown,550);
setTimeout(shopping_cart_dropdown,650);
setTimeout(shopping_cart_dropdown,950);




/***************** Search ******************/
	var $placeholder = $('#search input[type=text]').attr('data-placeholder');
	var logoHeight = parseInt($('#header-outer').attr('data-logo-height'));
	
	////search box event
	$('#search-btn').mousedown(function(){
		
		$(this).removeClass();
		
		$('#header-outer #search-outer').stop(true).fadeIn(500,'easeOutExpo');
		
		$('#search-outer > #search input[type="text"]').css({
			'top' : $('#search-outer').height()/2 - $('#search-outer > #search input[type="text"]').height()/2
		});
		
		$('#search input[type=text]').focus();
		
		if($('#search input[type=text]').attr('value') == $placeholder){
			$('#search input[type=text]').setCursorPosition(0);	
		}

		return false;
	});
	
	$('#search input[type=text]').keydown(function(){
		if($(this).attr('value') == $placeholder){
			$(this).attr('value', '');
		}
	});
	
	$('#search input[type=text]').keyup(function(){
		if($(this).attr('value') == ''){
			$(this).attr('value', $placeholder);
			$(this).setCursorPosition(0);
		}
	});
	
	
	////close search btn event
	$('#close').click(function(){
		
		closeSearch();
		return false;
	});

	//if user clicks away from the search close it
	$('#search-box input[type=text]').blur(function(e){
		closeSearch();
	});
	
	
	function closeSearch(){
		$('#header-outer #search-outer').stop(true).fadeOut(350,'easeOutExpo');
	}
	
	
	//mobile search
	$('#mobile-menu #mobile-search .container a#show-search').click(function(){
		$('#mobile-menu .container > ul').slideUp(500);
		return false;
	});
	
/***************** Nav ******************/
	
	var logoHeight = parseInt($('#header-outer').attr('data-logo-height'));
	var headerPadding = parseInt($('#header-outer').attr('data-padding'));
	var usingLogoImage = $('#header-outer').attr('data-using-logo');
	
	if( isNaN(headerPadding) || headerPadding.length == 0 ) { headerPadding = 28; }
	if( isNaN(logoHeight) || usingLogoImage.length == 0 ) { usingLogoImage = false; logoHeight = 30;}
	
	//inital calculations
	function headerInit() {
			
		$('#header-outer #logo img').css({
			'height' : logoHeight,				
		});	
		
		$('#header-outer').css({
			'padding-top' : headerPadding
		});	
		
		$('header#top nav > ul > li > a').css({
			'padding-bottom' : ((logoHeight/2) - ($('header#top nav > ul > li > a').height()/2)) + headerPadding,
			'padding-top' : (logoHeight/2) - ($('header#top nav > ul > li > a').height()/2)
		});	
		
		$('#header-outer .cart-menu').css({  
			'padding-bottom' : Math.ceil(((logoHeight/2) - ($('header#top nav > ul > li > a').height()/2)) + headerPadding),
			'padding-top' : Math.ceil(((logoHeight/2) - ($('header#top nav > ul > li > a').height()/2)) + headerPadding)
		});	
			
		$('header#top nav > ul li#search-btn').css({
			'padding-bottom' : (logoHeight/2) - ($('header#top nav > ul li#search-btn a').height()/2),
			'padding-top' : (logoHeight/2) - ($('header#top nav > ul li#search-btn a').height()/2)
		});	
		
		
		$('header#top .sf-menu > li > ul, header#top .sf-menu > li.sfHover > ul').css({
			'top' : $('header#top nav > ul > li > a').outerHeight() 
		});	
		
		//header space
		if($('#header-outer').attr('data-using-secondary') == '1'){
			$('#header-space').css('height', parseInt($('#header-outer').outerHeight()) + 34);
		} else {
			$('#header-space').css('height', $('#header-outer').outerHeight());
		}
		
		$('#header-outer .container, #header-outer .cart-menu').css('visibility','visible');
		
		
		$('#search-outer').css({
			'height' : logoHeight + headerPadding*2
		});	
		
		$('#search-outer > #search input[type="text"]').css({
			'font-size'  : 43,
			'top' : ((logoHeight + headerPadding*2)/2) - $('#search-outer > #search input[type="text"]').height()/2
		});
		
		$('#search-outer > #search #close a').css({
			'top' : ((logoHeight + headerPadding*2)/2) - 8
		});	
		
		
		//if no image is being used
		if(usingLogoImage == false) $('header#top #logo').css('margin-top','4px');
		
	}
	
	//one last check to make sure the header space is correct (only if the user hasn't scrolled yet)
	$(window).load(function(){
		if($(window).scrollTop() == 0 ) { 
			
			if($('#header-outer').attr('data-using-secondary') == '1'){
				$('#header-space').css('height', parseInt($('#header-outer').outerHeight()) + 34);
			} else {
				$('#header-space').css('height', $('#header-outer').outerHeight());
			}
			
		}
	});
	
	
	//is header resize on scroll enabled?
	var headerResize = $('#header-outer').attr('data-header-resize');
	if( headerResize == 1 ){
		
		headerInit();
		
		$(window).bind('scroll',smallNav);
		
		//if user starts in mobile but resizes to large, don't break the nav
		if($('body').hasClass('mobile')){
			$(window).resize(headerInit);
		}
		
	}
	
	else {
		headerInit();
	}
		

	function smallNav(){
		var $offset = $(window).scrollTop();
		var $windowWidth = $(window).width();
		var shrinkNum = 6;
		
		if (logoHeight >= 40 && logoHeight < 60) shrinkNum = 8;
		else if (logoHeight >= 60 && logoHeight < 80) shrinkNum = 10;
		else if (logoHeight >= 80 ) shrinkNum = 14;
		
		if($offset > 0 && $windowWidth > 1000) {
			
			$('#header-outer #logo img').stop(true,true).animate({
				'height' : logoHeight - shrinkNum
			},{queue:false, duration:250, easing: 'easeOutCubic'});	
				
			$('#header-outer').stop(true,true).animate({
				'padding-top' : headerPadding / 1.8
			},{queue:false, duration:250, easing: 'easeOutCubic'});	
			
			$('header#top nav > ul > li > a').stop(true,true).animate({
				'padding-bottom' : (((logoHeight-shrinkNum)/2) - ($('header#top nav > ul > li > a').height()/2)) + headerPadding / 1.8,
				'padding-top' : ((logoHeight-shrinkNum)/2) - ($('header#top nav > ul > li > a').height()/2)
			},{queue:false, duration:250, easing: 'easeOutCubic'});	
			
			$('#header-outer .cart-menu').stop(true,true).animate({
				'padding-bottom' : Math.floor((((logoHeight-shrinkNum)/2) - ($('header#top nav > ul > li > a').height()/2)) + headerPadding / 1.8),
				'padding-top' : Math.floor((((logoHeight-shrinkNum)/2) - ($('header#top nav > ul > li > a').height()/2)) + headerPadding / 1.8)
			},{queue:false, duration:250, easing: 'easeOutCubic'});	
			
			$('header#top nav > ul li#search-btn').stop(true,true).animate({
				'padding-bottom' : Math.floor(((logoHeight-shrinkNum)/2) - ($('header#top nav > ul li#search-btn a').height()/2)),
				'padding-top' : Math.floor(((logoHeight-shrinkNum)/2) - ($('header#top nav > ul li#search-btn a').height()/2))
			},{queue:false, duration:250, easing: 'easeOutCubic'});	
			
			$('header#top .sf-menu > li > ul, header#top .sf-menu > li.sfHover > ul').stop(true,true).animate({
				'top' : Math.floor($('header#top nav > ul > li > a').height() + (((logoHeight-shrinkNum)/2) - ($('header#top nav > ul > li > a').height()/2))*2 + headerPadding / 1.8),
			},{queue:false, duration:250, easing: 'easeOutCubic'});		
			
		

			$('#search-outer').stop(true,true).animate({
				'height' : (logoHeight-shrinkNum) + headerPadding
			},{queue:false, duration:450, easing: 'easeOutExpo'});	
			
			$('#search-outer > #search input[type="text"]').stop(true,true).animate({
				'font-size'  : 30,
				'line-height' : '30px',
				'top' : ((logoHeight-shrinkNum+headerPadding+5)/2) - ($('#search-outer > #search input[type="text"]').height()-15)/2
			},{queue:false, duration:450, easing: 'easeOutExpo'});	
			

			
			$('#search-outer > #search #close a, #search-outer > #search #close span').stop(true,true).animate({
				'top' : ((logoHeight-shrinkNum + headerPadding+5)/2) - 8
			},{queue:false, duration:450, easing: 'easeOutExpo'});	
			
			
			//if no image is being used
			if(usingLogoImage == false) $('header#top #logo').stop(true,true).animate({
				'margin-top' : 0
			},{queue:false, duration:450, easing: 'easeOutExpo'});	
			
			$(window).unbind('scroll',smallNav);
			$(window).bind('scroll',bigNav);
		}
	}
	
	function bigNav(){
		var $offset = $(window).scrollTop();
		var $windowWidth = $(window).width();
		if($offset == 0 && $windowWidth > 1000) {
			
			$('#header-outer #logo img').stop(true,true).animate({
				'height' : logoHeight,				
			},{queue:false, duration:250, easing: 'easeOutCubic'});	
			

			$('#header-outer').stop(true,true).animate({
				'padding-top' : headerPadding 
			},{queue:false, duration:250, easing: 'easeOutCubic'});	
			
			$('header#top nav > ul > li > a').stop(true,true).animate({
				'padding-bottom' : ((logoHeight/2) - ($('header#top nav > ul > li > a').height()/2)) + headerPadding,
				'padding-top' : (logoHeight/2) - ($('header#top nav > ul > li > a').height()/2)
			},{queue:false, duration:250, easing: 'easeOutCubic'});	
			
			$('#header-outer .cart-menu').stop(true,true).animate({
				'padding-bottom' : Math.ceil(((logoHeight/2) - ($('header#top nav > ul > li > a').height()/2)) + headerPadding),
				'padding-top' : Math.ceil(((logoHeight/2) - ($('header#top nav > ul > li > a').height()/2)) + headerPadding)
			},{queue:false, duration:250, easing: 'easeOutCubic'});	
			
			$('header#top nav > ul li#search-btn').stop(true,true).animate({
				'padding-bottom' : Math.floor((logoHeight/2) - ($('header#top nav > ul li#search-btn a').height()/2)),
				'padding-top' : Math.ceil((logoHeight/2) - ($('header#top nav > ul li#search-btn a').height()/2))
			},{queue:false, duration:250, easing: 'easeOutCubic'});	
			
			
			$('header#top .sf-menu > li > ul, header#top .sf-menu > li.sfHover > ul').stop(true,true).animate({
				'top' : $('header#top nav > ul > li > a').height() + (((logoHeight)/2) - ($('header#top nav > ul > li > a').height()/2))*2 + headerPadding,
			},{queue:false, duration:250, easing: 'easeOutCubic'});		
			
			
			
			$('#search-outer').stop(true,true).animate({
				'height' : logoHeight + headerPadding*2
			},{queue:false, duration:450, easing: 'easeOutExpo'});	
			
			$('#search-outer > #search input[type="text"]').stop(true,true).animate({
				'font-size'  : 43,
				'line-height' : '43px',
				'top' : ((logoHeight + headerPadding*2)/2) - 30
			},{queue:false, duration:450, easing: 'easeOutExpo'});	
			
			
			$('#search-outer > #search #close a, #search-outer > #search #close span').stop(true,true).animate({
				'top' : ((logoHeight + headerPadding*2)/2) - 8
			},{queue:false, duration:450, easing: 'easeOutExpo'});	
			
			//if no image is being used
			if(usingLogoImage == false) $('header#top #logo').stop(true,true).animate({
				'margin-top' : 4
			},{queue:false, duration:450, easing: 'easeOutExpo'});	
		
			$(window).unbind('scroll',bigNav);
			$(window).bind('scroll',smallNav);
		}
	}
	
	
	
	//responsive nav
	$('#toggle-nav').click(function(){
		
		$('#mobile-menu').stop(true,true).slideToggle(500);
		return false;
	});
	
	////append dropdown indicators / give classes
	$('#mobile-menu .container ul li').each(function(){
		if($(this).find('> ul').length > 0) {
			 $(this).addClass('has-ul');
			 $(this).find('> a').append('<span class="sf-sub-indicator"><i class="icon-angle-down"></i></span>');
		}
	});
	
	////events
	$('#mobile-menu .container ul li:has(">ul") > a .sf-sub-indicator').click(function(){
		$(this).parent().parent().toggleClass('open');
		$(this).parent().parent().find('> ul').stop(true,true).slideToggle();
		return false;
	});
	
	
	
	

	
/*-------------------------------------------------------------------------*/
/*	5.	Page Specific
/*-------------------------------------------------------------------------*/	

	//recent work
	function piVertCenter() {
		$('.portfolio-items  > .col').each(function(){
			var $colHeight = $(this).find('.work-item').height();
			var $infoHeight = $(this).find('.vert-center').height();
			
			//30px away from being centered so we can transition to center point on hover
			$(this).find('.work-info .vert-center').css('margin-top', (($colHeight / 2) - ($infoHeight / 2 )) - 30 );
		});	
	}
	
	$(window).load(function(){
	 	 piVertCenter();
	});
	 
	$(window).resize(function(){
		 piVertCenter();
		 portfolioDeviceCheck();
	});
	
	//portfolio item hover effect
	
	////desktop event 
	if(!$('body').hasClass('mobile')) {
		$('.portfolio-items .col .work-item').hover(function(){
			$(this).find('.work-info .vert-center').stop().animate({
				'padding-top' : 30
			},380,'easeOutCubic');
			$(this).find('.work-info').stop().animate({
				'opacity' : 1
			},380,'easeOutCubic');
			$(this).find('.work-info-bg').stop().animate({
				'opacity' : 0.9
			},380,'easeOutCubic');
		},function(){
			$(this).find('.work-info .vert-center').stop().animate({ 
				'padding-top' : 0
			},380,'easeOutCubic');
			$(this).find('.work-info').stop().animate({
				'opacity' : 0
			},380,'easeOutCubic');
			$(this).find('.work-info-bg').stop().animate({
				'opacity' : 0
			},380,'easeOutCubic');
		});
	} 
	////mobile event
	else {
		portfolioDeviceCheck();
	}
	
	function portfolioDeviceCheck(){
		if($('body').hasClass('mobile')){
			
			//if using more details
			if($('.portfolio-items .col .work-item').find('a:not(".pp")').length > 0){
				$('.portfolio-items .col .work-item').find('a.pp').css('display','none');
			} 
			
			//if only using pp
			else {
				$('.portfolio-items .col .work-item').find('a:not(".pp")').css('display','none');
			}
		
		} else {
			$('.portfolio-items .col .work-item').find('a').css('display','inline');
		}
	}

	
	//portfolio sort
	$('#portfolio-filters').hover(function(){
		$(this).find('ul').stop(true,true).slideDown(600,'easeOutExpo');
		$(this).find('a#sort-portfolio span').html($(this).find('a#sort-portfolio').attr('data-sortable-label'));
	},function(){
		var $activeCat = $(this).find('a.active').html();
		if( typeof $activeCat == 'undefined' || $activeCat.length == 0) $activeCat = $(this).attr('data-sortable-label');
		$(this).find('a#sort-portfolio span').html($activeCat);
		$(this).find('ul').stop(true,true).slideUp(600,'easeOutExpo');
	});
	
	//mobile sort menu fix
	if($('body').hasClass('mobile')){
		$('#portfolio-filters').unbind('mouseenter mouseleave');
		$('#portfolio-filters > a').click(function(){
			$(this).parent().find('ul').stop(true,true).slideToggle(600,'easeOutExpo');
		});
	}
	
	//portfolio selected category
	$('#portfolio-filters ul li a').click(function(){
		$(this).parents('#portfolio-filters').find('#sort-portfolio span').html($(this).html());
	});
	
	//portfolio more details page menu highlight
	$('body.single-portfolio #header-outer nav > ul > li > a:contains("Portfolio")').parents('li').addClass('current-menu-item');
	
	//blog love center
	function centerLove(){
		$('.post').each(function(){
			
			var $loveWidth = $(this).find('.post-meta .nectar-love').outerWidth();
			var $loveWrapWidth = $(this).find('.post-meta  .nectar-love-wrap').width();
			
			//center
			$(this).find('.post-meta .nectar-love').css('margin-left', $loveWrapWidth/2 - $loveWidth/2 + 'px' );
			$(this).find('.nectar-love-wrap').css('visibility','visible');
		});
	}
	
	$('.nectar-love').on('click',function(){
		centerLove();
	});
	
	centerLove();	
	
	
	//portfolio single comment order
	function portfolioCommentOrder(){
		
		if($('body').hasClass('mobile') && $('body').hasClass('single-portfolio') && $('#respond').length > 0){
			$('#sidebar').insertBefore('.comments-section');
		}
		 
		else if($('body').hasClass('single-portfolio') && $('#respond').length > 0) {
			$('#sidebar').insertAfter('#post-area');
		}
		
	}
	
	$(window).resize(portfolioCommentOrder);
	 portfolioCommentOrder();
	 
	
	//portfolio sidebar follow
	
	var sidebarFollow = $('.single-portfolio #sidebar').attr('data-follow-on-scroll');
	
	function portfolioSidebarFollow(){
	
		if( $('body.single-portfolio').length > 0 && sidebarFollow == 1 && !$('body').hasClass('mobile') && parseInt($('#sidebar').height()) + 50 <= parseInt($('#post-area').height())) {
			
			 $('#sidebar').addClass('fixed-sidebar');
			 
			 var $footer = '#footer-outer';
			 if( $('#call-to-action').length > 0 ) $footer = '#call-to-action';
			 
			 $('#sidebar').stickyMojo({footerID: $footer, contentID: '#post-area'});
			 
		}
		
	}
	
	$(window).load(portfolioSidebarFollow);
	
	
	//remove the portfolio filters that are not found in the current page
	var isotopeCatArr = [];
	$('#portfolio-filters ul li').each(function(i){
		isotopeCatArr[i] = $(this).find('a').attr('data-filter').substring(1);
	});
	
	////ice the first (all)
	isotopeCatArr.shift();
	
	
	var itemCats = '';
	
	$('#portfolio > div').each(function(i){
		itemCats += $(this).attr('data-project-cat');
	});
	itemCats = itemCats.split(' ');
	
	////remove the extra item on the end of blank space
	itemCats.pop();
	
	////make sure the array has no duplicates
	itemCats = $.unique(itemCats);
	
	
	////Find which categories are actually on the current page
	var notFoundCats = [];
	$.grep(isotopeCatArr, function(el) {

    	if ($.inArray(el, itemCats) == -1) notFoundCats.push(el);

	});
	
	//manipulate the list
	if(notFoundCats.length != 0){
		$('#portfolio-filters ul li').each(function(){
			if( $.inArray($(this).find('a').attr('data-filter').substring(1), notFoundCats) != -1 ){ $(this).hide(); }
		})
	}
	
	
	
	//sharing buttons
	
	var completed = 0;
	
	if( $('#project-meta').attr('data-sharing') == '1' || $('#single-meta').attr('data-sharing') == '1') {

	
		////facebook
		
		//load share count on load  
		$.getJSON("http://graph.facebook.com/?id="+ window.location +'&callback=?', function(data) {
			if((data.shares != 0) && (data.shares != undefined) && (data.shares != null)) { 
				$('.facebook-share a span.count').html( data.shares );	
			}
			else {
				$('.facebook-share a span.count').html( 0 );	
			}
			completed++;
			socialFade();
		});
	 
		function facebookShare(){
			window.open( 'https://www.facebook.com/sharer/sharer.php?u='+window.location, "facebookWindow", "height=380,width=660,resizable=0,toolbar=0,menubar=0,status=0,location=0,scrollbars=0" ) 
			return false;
		}
		
		$('.facebook-share').click(facebookShare);
		
		////twitter
		
		//load tweet count on load 
		$.getJSON('http://urls.api.twitter.com/1/urls/count.json?url='+window.location+'&callback=?', function(data) {
			if((data.count != 0) && (data.count != undefined) && (data.count != null)) { 
				$('.twitter-share a span.count').html( data.count );
			}
			else {
				$('.twitter-share a span.count').html( 0 );
			}
			completed++;
			socialFade();
		});
		
		function twitterShare(){
			window.open( 'http://twitter.com/intent/tweet?text='+$(".section-title h1").text() +' '+window.location, "twitterWindow", "height=380,width=660,resizable=0,toolbar=0,menubar=0,status=0,location=0,scrollbars=0" ) 
			return false;
		}
		function wooTwitterShare(){
			window.open( 'http://twitter.com/intent/tweet?text='+$("h1.product_title").text() +' '+window.location, "twitterWindow", "height=380,width=660,resizable=0,toolbar=0,menubar=0,status=0,location=0,scrollbars=0" ) 
			return false;
		}
		
		$('.twitter-share:not(".woo")').click(twitterShare);
		$('.twitter-share.woo').click(wooTwitterShare);
		
		////pinterest
		
		//load pin count on load 
		$.getJSON('http://api.pinterest.com/v1/urls/count.json?url='+window.location+'&callback=?', function(data) {
			if((data.count != 0) && (data.count != undefined) && (data.count != null)) { 
				$('.pinterest-share a span.count').html( data.count );
			}
			else {
				$('.pinterest-share a span.count').html( 0 );
			}
			completed++;
			socialFade();
		});
		
		function pinterestShare(){
			window.open( 'http://pinterest.com/pin/create/button/?url='+window.location+'&media='+$('#post-area img').first().attr('src')+'&description='+$('.section-title h1').text(), "pinterestWindow", "height=640,width=660,resizable=0,toolbar=0,menubar=0,status=0,location=0,scrollbars=0" ) 
			return false;
		}
		
		function wooPinterestShare(){
			window.open( 'http://pinterest.com/pin/create/button/?url='+window.location+'&media='+$('img.attachment-shop_single').first().attr('src')+'&description='+$('h1.product_title').text(), "pinterestWindow", "height=640,width=660,resizable=0,toolbar=0,menubar=0,status=0,location=0,scrollbars=0" ) 
			return false;
		}
		
		$('.pinterest-share:not(".woo")').click(pinterestShare);
		$('.pinterest-share.woo').click(wooPinterestShare);
		
		//fadeIn
		$('.sharing li a > span.count, .sharing .nectar-love-count, #single-meta ul li a span').hide().css('width','auto');
		function socialFade(){
			
			if(completed == $('#project-meta .sharing li').length - 1 || completed == $('#single-meta ul li').length - 1 ) {
				
				//love fadein
				$('#project-meta ul li .nectar-love-wrap.fadein .nectar-love-count ').show(220,'easeOutSine',function(){
					$(this).animate({'opacity':1},800);
				});
				
				//sharing loadin
				$('.sharing li, #single-meta ul li').each(function(i){
					var $that = $(this);
					
					$(this).find('a').animate({'left': 0},220,'easeOutSine');
					$(this).find('a > span').show(220,'easeOutSine',function(){
						$that.find('a > span').animate({'opacity':1},800);
					});
					
				});
			}
		}
	}
	
/*-------------------------------------------------------------------------*/
/*	6.	Scroll to top
/*-------------------------------------------------------------------------*/	

var $scrollTop = $(window).scrollTop();

//starting bind
if( $('#to-top').length > 0 && $(window).width() > 1020) {
	
	if($scrollTop > 350){
		$(window).bind('scroll',hideToTop);
	}
	else {
		$(window).bind('scroll',showToTop);
	}
}


function showToTop(){
	
	if( $scrollTop > 350 ){

		$('#to-top').stop(true,true).animate({
			'bottom' : '17px'
		},350,'easeInOutCubic');	
		
		$(window).unbind('scroll',showToTop);
		$(window).bind('scroll',hideToTop);
	}

}

function hideToTop(){
	
	if( $scrollTop < 350 ){

		$('#to-top').stop(true,true).animate({
			'bottom' : '-30px'
		},350,'easeInOutCubic');	
		
		$(window).unbind('scroll',hideToTop);
		$(window).bind('scroll',showToTop);	
		
	}
}

//to top color
if( $('#to-top').length > 0 ) {
	
	var $windowHeight, $pageHeight, $footerHeight, $ctaHeight;
	
	function calcToTopColor(){
		$scrollTop = $(window).scrollTop();
		$windowHeight = $(window).height();
		$pageHeight = $('body').height();
		$footerHeight = $('#footer-outer').height();
		$ctaHeight = ($('#call-to-action').length > 0) ? $('#call-to-action').height() : 0;
		
		if( ($scrollTop-35 + $windowHeight) >= ($pageHeight - $footerHeight) && $('#boxed').length == 0){
			$('#to-top').addClass('dark');
		}
		
		else {
			$('#to-top').removeClass('dark');
		}
	}
	
	//calc on scroll
	$(window).scroll(calcToTopColor);
	
	//calc on resize
	$(window).resize(calcToTopColor);

}

//scroll up event
$('#to-top').click(function(){
	$('body,html').stop().animate({
		scrollTop:0
	},800,'easeOutCubic')
	return false;
});

/*-------------------------------------------------------------------------*/
/*	7.	Cross Browser Fixes
/*-------------------------------------------------------------------------*/	
	 
	//Fix current class in menu 
	if ($("body").hasClass("single-portfolio") || $('body').hasClass("error404") || $('body').hasClass("search-results")) {   
		$("li").removeClass("current_page_parent").removeClass("current-menu-ancestor").removeClass('current_page_ancestor');   
	}
	
	//fix for IE8 nth-child
	$('.recent_projects_widget div a:nth-child(3n+3), #sidebar #flickr div:nth-child(3n+3) a, #footer-outer #flickr div:nth-child(3n+3) a').css('margin-right','0px');
	
	//remove br's from code tag
	$('code').find('br').remove();	
	
	//if a clear is the last div, remove the padding
	if($('.container.main-content > .row > div:last-child').hasClass('clear')) {
		$('.container.main-content > .row > div:last-child').css('padding-bottom','0');
	}
	
	//homepage recent blog for IE8
	$('.home-wrap .blog-recent > div:last-child').addClass('col_last');
	
	//contact form
	$('.wpcf7-form p:has(input[type=submit])').css('padding-bottom','0px');
	
	//no caption home slider fix
	$('#featured article').each(function(){
		if($(this).find('h2').attr('data-has-caption') == '0') {
			$(this).parents('.slide').addClass('no-caption');
		}
	});
	
	//chat post format nth child color
	$('article.post.format-chat .content-inner dt:odd').css('color','#333');
	
	//remove margin on last cols inside of full width sections 
	$('.full-width-section').each(function(){
		$(this).find('> .span_12 > div.col_last').last().css('margin-bottom','0');
	});
	
	//remove p tags from extra content editor when warpping only an img 
	$('#portfolio-extra p').each(function(){
		if($(this).find('*').length == 1 && $(this).find('img').length == 1) {
			$(this).find('img').unwrap();
		}
	});
	
	//carousel head button alignment  
	$('.carousel-heading').each(function(){
		if($(this).find('h2').length > 0) $(this).find('.carousel-prev, .carousel-next').css('top','5px');
	});
	
	//woocommerce product thuimbnails
	$('.woocommerce div.product div.images div.thumbnails a:nth-child(4n+4)').css('margin-right','0px');
	
	//remove extra galleries when using the nectar gallery slider on projects and posts
	$('article.post .gallery-slider .gallery,  article.post .gallery-slider .jetpack-slideshow, .single-portfolio .gallery-slider .gallery, .single-portfolio .gallery-slider .jetpack-slideshow').remove();
	
	
	$('.woocommerce .span_9 .products.related .products li:nth-child(4), .woocommerce .span_9 .products.upsells .products li:nth-child(4)').remove();
	$('.woocommerce .span_9 .products.related .products li:nth-child(3), .woocommerce .span_9 .products.upsells .products li:nth-child(3)').css('margin-right','0');	
	
	//clients no hover if no link
	$('div.clients').each(function(){
		$(this).find('> div').each(function(){
			if($(this).find('a').length == 0) {
				$(this).addClass('no-link');
			}
		});
	});
	 
	
	
	
	
	
});



function resizeIframe() {
	var element = document.getElementById("pp_full_res").getElementsByTagName("iframe");
	var height = element[0].contentWindow.document.body.scrollHeight;
    
    //iframe height
    element[0].style.height = height + 'px';
	
	//pp height
	document.getElementsByClassName("pp_content_container")[0].style.height = height+40+ 'px';
	document.getElementsByClassName("pp_content")[0].style.height = height+40+ 'px';
	
}

(function(e){e.fn.hoverIntent=function(t,n,r){var i={interval:100,sensitivity:7,timeout:0};if(typeof t==="object"){i=e.extend(i,t)}else if(e.isFunction(n)){i=e.extend(i,{over:t,out:n,selector:r})}else{i=e.extend(i,{over:t,out:t,selector:n})}var s,o,u,a;var f=function(e){s=e.pageX;o=e.pageY};var l=function(t,n){n.hoverIntent_t=clearTimeout(n.hoverIntent_t);if(Math.abs(u-s)+Math.abs(a-o)<i.sensitivity){e(n).off("mousemove.hoverIntent",f);n.hoverIntent_s=1;return i.over.apply(n,[t])}else{u=s;a=o;n.hoverIntent_t=setTimeout(function(){l(t,n)},i.interval)}};var c=function(e,t){t.hoverIntent_t=clearTimeout(t.hoverIntent_t);t.hoverIntent_s=0;return i.out.apply(t,[e])};var h=function(t){var n=jQuery.extend({},t);var r=this;if(r.hoverIntent_t){r.hoverIntent_t=clearTimeout(r.hoverIntent_t)}if(t.type=="mouseenter"){u=n.pageX;a=n.pageY;e(r).on("mousemove.hoverIntent",f);if(r.hoverIntent_s!=1){r.hoverIntent_t=setTimeout(function(){l(n,r)},i.interval)}}else{e(r).off("mousemove.hoverIntent",f);if(r.hoverIntent_s==1){r.hoverIntent_t=setTimeout(function(){c(n,r)},i.timeout)}}};return this.on({"mouseenter.hoverIntent":h,"mouseleave.hoverIntent":h},i.selector)}})(jQuery)