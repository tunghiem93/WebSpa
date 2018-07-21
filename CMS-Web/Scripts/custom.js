jQuery(document).ready(function($){
	
	var isMobile = (navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i)) || (navigator.userAgent.match(/Android/i)) || (navigator.userAgent.match(/Blackberry/i)) || (navigator.userAgent.match(/Windows Phone/i)) ? true : false;
	
	var currentWidth = window.innerWidth || document.documentElement.clientWidth;

	$("select").each(function(){
		if($(this).css('display') != 'none') {
			$(this).wrap( '<div class="selection-box"></div>' );
		}
	});
	
	//Menu Start
	megaMenu();
	function megaMenu() {
		var screenWidth = $(document).width(),
		containerWidth = $("#header .container").width(),
		containerMinuScreen = (screenWidth - containerWidth)/2;
		if( containerWidth == screenWidth ){

			$px = mytheme_urls.scroll == "disable" ? 45 : 25;
			
			$("li.menu-item-megamenu-parent .megamenu-child-container").each(function(){

				var ParentLeftPosition = $(this).parent("li.menu-item-megamenu-parent").offset().left,
				MegaMenuChildContainerWidth = $(this).width();

				if( (ParentLeftPosition + MegaMenuChildContainerWidth) > screenWidth ){
					var SwMinuOffset = screenWidth - ParentLeftPosition;
					var marginFromLeft = MegaMenuChildContainerWidth - SwMinuOffset;
					var marginFromLeftActual = (marginFromLeft) + $px;
					var marginLeftFromScreen = "-"+marginFromLeftActual+"px";
					$(this).css('left',marginLeftFromScreen);
				}

			});
		} else {

			$px = mytheme_urls.scroll == "disable" ? 40 : 20;

			$("li.menu-item-megamenu-parent .megamenu-child-container").each(function(){
				var ParentLeftPosition = $(this).parent("li.menu-item-megamenu-parent").offset().left,
				MegaMenuChildContainerWidth = $(this).width();

				if( (ParentLeftPosition + MegaMenuChildContainerWidth) > containerWidth ){
					var marginFromLeft = ( ParentLeftPosition + MegaMenuChildContainerWidth ) - screenWidth;
					var marginLeftFromContainer = containerMinuScreen + marginFromLeft + $px;

					if( MegaMenuChildContainerWidth > containerWidth ){
						var MegaMinuContainer	= ( (MegaMenuChildContainerWidth - containerWidth)/2 ) + 10;
						var marginLeftFromContainerVal = marginLeftFromContainer - MegaMinuContainer;
						marginLeftFromContainerVal = "-"+marginLeftFromContainerVal+"px";
						$(this).css('left',marginLeftFromContainerVal);
					} else {
						marginLeftFromContainer = "-"+marginLeftFromContainer+"px";
						$(this).css('left',marginLeftFromContainer);
					}
				}

			});
		}
	}
	
	//Menu Hover Start
	function menuHover() {
		$("li.menu-item-depth-0,li.menu-item-simple-parent ul li" ).hover(
			function(){
				if( $(this).find(".megamenu-child-container").length  ){
					$(this).find(".megamenu-child-container").stop().fadeIn('fast');
				} else {
					$(this).find("> ul.sub-menu").stop().fadeIn('fast');
				}
			},
			function(){
				if( $(this).find(".megamenu-child-container").length ){
					$(this).find(".megamenu-child-container").stop(true, true).hide();
				} else {
					$(this).find('> ul.sub-menu').stop(true, true).hide(); 
				}
			}
		);
	}//Menu Hover End

	//Sticky Navigation
	if( navigator.platform.match(/(Mac|iPhone|iPod|iPad)/i) || 
		navigator.userAgent.match(/Android/i)||
		navigator.userAgent.match(/webOS/i) || 
		navigator.userAgent.match(/iPhone/i) || 
		navigator.userAgent.match(/iPod/i)) {
			if( mytheme_urls.mobilestickynav === "enable") {
				$("#main-menu").sticky({ topSpacing: 0 });
			}
	} else if( mytheme_urls.stickynav === "enable" && currentWidth > 767 ) {
			$("#main-menu").sticky({ topSpacing: 0 });
	}	
	//Sticky Navigation End
	
	//Mobile Menu
	$("#dt-menu-toggle").click(function( event ){
		event.preventDefault();
		$menu = $("nav#main-menu").find("ul.menu:first");
		$menu.slideToggle(function(){
			duration: 900;
			//$menu.css('overflow' , 'visible');
			$menu.toggleClass('menu-toggle-open');
		});
	});

	$(".dt-menu-expand").click(function(){
		if( $(this).hasClass("dt-mean-clicked") ){
			$(this).text("+");
			if( $(this).prev('ul').length ) {
				$(this).prev('ul').slideUp(300);
			} else {
				$(this).prev('.megamenu-child-container').find('ul:first').slideUp(300);
			}
		} else {
			$(this).text("-");
			if( $(this).prev('ul').length ) {
				$(this).prev('ul').slideDown(300);
			} else{
				$(this).prev('.megamenu-child-container').find('ul:first').slideDown(300);
			}
		}
		
		$(this).toggleClass("dt-mean-clicked");
		return false;
	});

	if( !isMobile ){
		currentWidth = window.innerWidth || document.documentElement.clientWidth;
		if( currentWidth > 767 ){
			menuHover();
		}
	}
	//Mobile Menu End
//Menu End

$().UItoTop({ easingType: 'easeOutQuart' });

//Nice Scroll
	var isMacLike = navigator.platform.match(/(Mac|iPhone|iPod|iPad)/i)?true:false;
	if( mytheme_urls.scroll === "enable" && !isMacLike ) {
		jQuery("html").niceScroll({zindex:99999,cursorborder:"1px solid #424242"});
	}
//Nice Scroll End

//Blog Template
	if( $(".apply-isotope").length ) {
		$(".apply-isotope").isotope({itemSelector : '.column',transformsEnabled:false,masonry: { gutterWidth: 20} });
	}

//Gallery Post Slider
    if( ($("ul.entry-gallery-post-slider").length) && ( $("ul.entry-gallery-post-slider li").length > 1 ) ){
	  	$("ul.entry-gallery-post-slider").bxSlider({auto:false, video:true, useCSS:false, pager:'', autoHover:true, adaptiveHeight:true});
    }	

//Gallery Template
	if (Modernizr.touch) {
		$(".close-overlay").removeClass("hidden");
		
		$(".dt-gallery").click(function(e){
			$(".gallery > .hover").removeClass("hover");
			
			if (!$(this).hasClass("hover")) {
				$(this).addClass("hover");
			}
	    });
		
		$(".close-overlay").click(function(e){
			if ($(this).closest(".dt-gallery").hasClass("hover")) {
				$(this).closest(".dt-gallery").removeClass("hover");
			}
			e.stopPropagation();
			e.preventDefault();
		});
	} else {
		$(".dt-gallery").mouseenter(function(){
			 $(this).addClass("hover");
		}).mouseleave(function(){
			 $(this).removeClass("hover");
		});
	}
	
//Gallery prettyPhoto
	if($(".gallery").length) {
		$(".gallery a[data-gal^='prettyPhoto']").prettyPhoto({hook:'data-gal',animation_speed:'normal',theme:'light_square',slideshow:3000, autoplay_slideshow: false,social_tools: false,deeplinking:false});		
	}

// Gallery Slider
	if( $(".dt-gallery-single-slider").find("li").length > 1 ) {
		$(".dt-gallery-single-slider").bxSlider({ auto:false, video:true, useCSS:false, pagerCustom: '#bx-pager', autoHover:true, adaptiveHeight:true, controls:false });
	}

// Related Gallery Slider
	if( $(".dt-sc-gallery-carousel").find("li").length > 3 ) {
		$(".dt-sc-gallery-carousel").carouFredSel({
			responsive: true,
			auto: false,
			width: '100%',
			height: 'variable',
			prev: '.dt-gallery-prev',
			next: '.dt-gallery-next',
			height: 'auto',
			scroll: 1,
			items: { width:340, height: 'variable', visible: { min: 1, max:3 } }
		});
	}	

//Gallery Template

//FitVids
 	$("div.dt-video-wrap").fitVids();
 	$('.wp-video').css('width', '100%');

//Smart Resize Start
	$(window).smartresize(function(){
		
		$("ul.menu").removeAttr("style");
		
		megaMenu();
		
		//Mobile Menu
		currentWidth = window.innerWidth || document.documentElement.clientWidth;
		if( !isMobile && (currentWidth > 767)  ){
			menuHover();
		}
		
		//Blog Template
		if( $(".apply-isotope").length ) {
			$(".apply-isotope").isotope({itemSelector : '.column',transformsEnabled:false,masonry: { gutterWidth: 20} });
		}

		//Gallery Template
		if( $('.dt-sc-gallery-container').length ) {
			$width = $('.dt-sc-gallery-container').hasClass("no-space") ? 0 : 20;
			$('.dt-sc-gallery-container').css({overflow:'hidden'}).isotope({itemSelector : '.column',masonry: { gutterWidth: $width } });
		}		
	});
//Smart Resize End

// Window Load Start
	$(window).load(function() {

		//Blog Template
		if( $(".apply-isotope").length ) {
			$(".apply-isotope").isotope({itemSelector : '.column',transformsEnabled:false,masonry: { gutterWidth: 20} });
		}
		//Blog Template End

		//Gallery Template
			//Isotope
			var $container = $('.dt-sc-gallery-container');
			if( $container.length) {
				$width = $container.hasClass("no-space") ? 0 : 20;
				$container.isotope({
					filter: '*',
					masonry: { gutterWidth: $width },
					animationOptions: { duration: 750, easing: 'linear', queue: false  }
				});
			}//Isotope End

			if($("div.dt-sc-sorting-container").length){
				$("div.dt-sc-sorting-container a").click(function(){
					$("div.dt-sc-sorting-container a").removeClass("active-sort");
					var $width = $container.hasClass("no-space") ? 0 : 20;
					var selector = $(this).attr('data-filter');
					$(this).addClass("active-sort");

					$('.dt-sc-gallery-container').isotope({
						filter: selector,
						masonry: { gutterWidth: $width },
						animationOptions: { duration:750, easing: 'linear',  queue: false }
					});
					return false;
				});
			}
		//Gallery Template End	
	});
// Window Load End
});