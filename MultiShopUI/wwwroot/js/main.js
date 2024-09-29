(function ($) {
    "use strict";


    // Dropdown on mouse hover
    $(document).ready(function () {
        function toggleNavbarMethod() {
            if ($(window).width() > 768) {
                $('.navbar .dropdown').on('mouseover', function () {
                    $('.dropdown-toggle', this).trigger('click');
                }).on('mouseout', function () {
                    $('.dropdown-toggle', this).trigger('click').blur();
                });
            } else {
                $('.navbar .dropdown').off('mouseover').off('mouseout');
            }
        }
        toggleNavbarMethod();
        $(window).resize(toggleNavbarMethod);
    });
    
    
    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({scrollTop: 0}, 1500, 'easeInOutExpo');
        return false;
    });
    
    
    // Home page slider
    $('.main-slider').slick({
        autoplay: true,
        dots: true,
        infinite: true,
        slidesToShow: 1,
        slidesToScroll: 1,
        centerMode: true,
        variableWidth: true
    });
    
    
    // Product Slider 4 Column
    $('.product-slider-4').slick({
        autoplay: true,
        infinite: true,
        dots: false,
        slidesToShow: 4,
        slidesToScroll: 1,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    slidesToShow: 4,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    slidesToShow: 3,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 2,
                }
            },
            {
                breakpoint: 576,
                settings: {
                    slidesToShow: 1,
                }
            },
        ]
    });
    
    
    // Product Slider 3 Column
    $('.product-slider-3').slick({
        autoplay: true,
        infinite: true,
        dots: false,
        slidesToShow: 3,
        slidesToScroll: 1,
        responsive: [
            {
                breakpoint: 992,
                settings: {
                    slidesToShow: 3,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 2,
                }
            },
            {
                breakpoint: 576,
                settings: {
                    slidesToShow: 1,
                }
            },
        ]
    });
    
    
    // Single Product Slider
    $('.product-slider-single').slick({
        infinite: true,
        dots: false,
        slidesToShow: 1,
        slidesToScroll: 1
    });
    
    
    // Brand Slider
    $('.brand-slider').slick({
        speed: 1000,
        autoplay: true,
        autoplaySpeed: 1000,
        infinite: true,
        arrows: false,
        dots: false,
        slidesToShow: 5,
        slidesToScroll: 1,
        responsive: [
            {
                breakpoint: 992,
                settings: {
                    slidesToShow: 4,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 3,
                }
            },
            {
                breakpoint: 576,
                settings: {
                    slidesToShow: 2,
                }
            },
            {
                breakpoint: 300,
                settings: {
                    slidesToShow: 1,
                }
            }
        ]
    });
    
    ////quantity
    //$('.qty button').on('click', function () {
    //    var $button = $(this);
    //    var oldValue = $button.parent().find('input').val();

    //    if ($button.hasClass('btn-plus')) {
    //        var newVal = parseInt(oldValue) + 1;  // Her defe 1 artir
    //    } else {
    //        if (oldValue > 1) {  // Miqdar minimum 1 ola biler
    //            var newVal = parseInt(oldValue) - 1; // Her defe 1 azalir
    //        } else {
    //            newVal = 1;  // Miqdar hec vaxt 0 olmasin
    //        }
    //    }

    //    $button.parent().find('input').val(newVal);
    //    $('#hiddenQuantityInput').val(newVal); // Gizli inputu da yenil?
    //});
    
    // Shipping address show hide
    $('.checkout #shipto').change(function () {
        if($(this).is(':checked')) {
            $('.checkout .shipping-address').slideDown();
        } else {
            $('.checkout .shipping-address').slideUp();
        }
    });
    
    
    // Payment methods show hide
    $('.checkout .payment-method .custom-control-input').change(function () {
        if ($(this).prop('checked')) {
            var checkbox_id = $(this).attr('id');
            $('.checkout .payment-method .payment-content').slideUp();
            $('#' + checkbox_id + '-show').slideDown();
        }
    });


    // Logout function
    function logout() {
        // localStorage v? ya sessionStorage-da saxlan?lan token silinir
        localStorage.removeItem('accessToken');
        sessionStorage.removeItem('accessToken');

        // ?stifad?çini logout s?hif?sin? yönl?ndirin
        window.location.href = "/Auth/Logout";
    }

    // Logout button event
    $('#logoutButton').click(function (event) {
        event.preventDefault();
        logout();
    });

    function loadContent(tab) {
        var url = '';
        switch (tab) {
            case 'orders':
                url = '/Account/Account'; 
                break;
            case 'payment':
                url = '/Account/PaymentMethods'; 
                break;
            case 'address':
                url = '/Account/ShippingAddress'; 
                break;
            case 'account':
                url = '/Account/Account'; 
                break;
        }

        if (url) {
            $.ajax({
                url: url,
                type: 'GET',
                success: function (data) {
                    $('#contentArea').html(data); // 
                },
                error: function () {
                    alert('Error loading content!');
                }
            });
        }
    }

    //function updateQuantity(amount) {
    //    var quantityInput = document.getElementById("quantityInput");
    //    var hiddenQuantityInput = document.getElementById("hiddenQuantityInput");
    //    var currentQuantity = parseInt(quantityInput.value);
    //    var newQuantity = currentQuantity + amount;

    //    if (newQuantity >= 1) { // Miqdar?n 0-dan az olmamas? üçün
    //        quantityInput.value = newQuantity;
    //        hiddenQuantityInput.value = newQuantity; // Form il? gönd?ril?c?k gizli input
    //    }
    //}


})(jQuery);

