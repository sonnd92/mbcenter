(function($){
    var slidebnHome = {
        init: function () {
            slidebnHome.events();
        },
        events: function () {
            $('.spbnhome').swiper({
                slidesPerView: 1,
                autoheight: true,
                preventClicks: false,
                paginationClickable: true,
                spaceBetween: 5,
                centeredSlides: true,
                autoplay: 3000,
                autoplayDisableOnInteraction: false,
                nextButton: '.sp-bn-next',
                prevButton: '.sp-bn-prev'
            });
        }
    };

    var slideykkh = {
        init: function () {
            slideykkh.events();
        },
        events: function () {
            $('.sp-khcmt-sl').swiper({
                slidesPerView: 3,
                autoheight: true,
                preventClicks: false,
                paginationClickable: true,
                spaceBetween: 15,
                nextButton: '.sp-khcmt .sp-khcmt-next',
                prevButton: '.sp-khcmt .sp-khcmt-prev',
                breakpoints: {
                    992: {
                        slidesPerView: 2,
                        spaceBetween: 10
                    },
                    550: {
                        slidesPerView: 1,
                        spaceBetween: 0
                    }
                }
            });
        }
    };

    var slideLNykkh = {
        init: function () {
            slideLNykkh.events();
        },
        events: function () {
            $('.sp-ykkh-sl').swiper({
                slidesPerView: 1,
                autoheight: true,
                preventClicks: false,
                paginationClickable: true,
                spaceBetween: 0,
                autoplay: 3000,
                autoplayDisableOnInteraction: false,
                nextButton: '.sp-ykcmt .sp-khcmt-next',
                prevButton: '.sp-ykcmt .sp-khcmt-prev',
                breakpoints: {
                    992: {
                        slidesPerView: 1,
                        spaceBetween: 5
                    },
                    768: {
                        slidesPerView: 2,
                        spaceBetween: 15
                    },
                    550: {
                        slidesPerView: 1,
                        spaceBetween: 0
                    }
                }
            });
        }
    };

    var slidehnews = {
        init: function () {
            slidehnews.events();
        },
        events: function () {
            $('.sp-homenews').swiper({
                slidesPerView: 2,
                slidesPerColumn: 2,
                autoheight: true,
                preventClicks: false,
                paginationClickable: true,
                spaceBetween: 30,
                nextButton: '.sp-homenews-next',
                prevButton: '.sp-homenews-prev',
                breakpoints: {
                    680: {
                        slidesPerView: 2,
                        spaceBetween: 30
                    },
                    520: {
                        slidesPerView: 1,
                        spaceBetween: 30
                    }
                }
            });
        }
    };

    var albumHome = {
        init: function () {
            albumHome.events();
        },
        events: function () {
            $(".fancybox-thumb").fancybox({
                prevEffect	: 'none',
                nextEffect	: 'none',
                helpers	: {
                    title	: {
                        type: 'outside'
                    },
                    thumbs	: {
                        width	: 50,
                        height	: 50
                    }
                }
            });
        }
    };

    var videoHome = {
        init: function () {
            videoHome.events();
        },
        events: function () {
            $(".various").fancybox({
                maxWidth	: 800,
                maxHeight	: 600,
                fitToView	: false,
                width		: '70%',
                height		: '70%',
                autoSize	: false,
                closeClick	: false,
                openEffect	: 'none',
                closeEffect	: 'none'
            });
        }
    };

    var dtptime = {
        init: function () {
            dtptime.events();
        },
        events: function () {
            $('#datetimepicker4').datetimepicker();
        }
    };

	$(document).ready(function (){
        if ($(".spbnhome").length > 0) {
            slidebnHome.init();
        }
        if ($(".sp-khcmt-sl").length > 0) {
            slideykkh.init();
        }
        if ($(".sp-homenews").length > 0) {
            slidehnews.init();
        }
        if ($(".fancybox-thumb").length > 0) {
            albumHome.init();
        }
        if ($(".various").length > 0) {
            videoHome.init();
        }
        if ($("#datetimepicker4").length > 0) {
            dtptime.init();
        }
        if ($(".sp-ykkh-sl").length > 0) {
            slideLNykkh.init();
        }

        $('.sp-navmnicon').click(function(){
            $('.sp-header').toggleClass('sp-hmnshow');
        });

        $('.sp-navmnsub').click(function(){
            $(this).parent().toggleClass('sp-nmnshow');
        });

        $(".sp-dtsv-bg a").click(function (){
            $('html, body').animate({ scrollTop: $(".sp-dtsv-bgtit").offset().top - 10 }, 1000);
        });

        $(window).scroll(function () {
            if ($(this).scrollTop() > 100) {
                $('.sp-totop').fadeIn();
            } else {
                $('.sp-totop').fadeOut();
            }
        });
        $('.sp-totop').click(function () {
            $("html, body").animate({ scrollTop: 0 }, 600);
            return false;
        });

        wow = new WOW({
            animateClass: 'animated',
            offset: 5
        });
        wow.init();



	});
})(window.jQuery);