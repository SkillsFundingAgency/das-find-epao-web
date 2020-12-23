var forms = $('.validate-auto-complete');

var idSelectField = 'SelectedCourseId';

var selectEl = document.querySelector('#' + idSelectField);
if (selectEl) {
    accessibleAutocomplete.enhanceSelectElement({
        selectElement: selectEl,
        minLength: 3,
        autoselect: true,
        defaultValue: '',
        displayMenu: 'overlay',
        placeholder: 'Start typing to search for an apprenticeship training course',
        onConfirm: function (opt) {
            var txtInput = document.querySelector('#' + idSelectField);
            var searchString = opt || txtInput.value;
            var requestedOption = [].filter.call(this.selectElement.options,
                function (option) {
                    return (option.textContent || option.innerText) === searchString;
                }
            )[0];
            if (requestedOption) {
                requestedOption.selected = true;
            } else {
                this.selectElement.selectedIndex = 0;
            }
        }
    });

    forms.on('submit',
        function(e) {

            $('.autocomplete__input').each(function() {
                var that = $(this);
                if (that.val().length === 0) {
                    var fieldId = that.attr('id'),
                        selectField = $('#' + fieldId + '-select');
                    selectField[0].selectedIndex = 0;
                }
            });
        });
}

forms.attr('novalidate', 'novalidate');



// BACK LINK
// If users history-1 does not come from this site, 
// then show a link to homepage

var $backLinkOrHome = $('.das-js-back-link-or-home');
var backLinkOrHome = function () {

    var referrer = document.referrer;

    var backLink = $('<a>')
        .attr({'href': '#', 'class': 'govuk-back-link'})
        .text('Home')
        .on('click', function (e) {
            window.history.back();
            e.preventDefault();
        });

    if (referrer && referrer !== document.location.href) {
        $backLinkOrHome.replaceWith(backLink);
    }
}

if ($backLinkOrHome) {
    backLinkOrHome();
}


// BACK TO TOP 
// Shows a back-to-top link in a floating header
// as soon as the breadcrumbs scroll out of view

$(window).bind('scroll', function() {

    var isCookieBannerVisible = $('.das-cookie-banner:visible').length,
        showHeaderDistance = 150 + (isCookieBannerVisible * 240),
        $breadcrumbs = $('.govuk-breadcrumbs');

    if ($breadcrumbs.length > 0) {
        var breadcrumbDistanceFromTop = $breadcrumbs.offset().top,
            breadcrumbHeight = $breadcrumbs.outerHeight();

        showHeaderDistance = breadcrumbDistanceFromTop + breadcrumbHeight;
    }

    if ($(window).scrollTop() > showHeaderDistance) {
        $('.app-shortlist-banner').addClass('app-shortlist-banner__fixed');
    } else {
        $('.app-shortlist-banner').removeClass('app-shortlist-banner__fixed');
    }

}).trigger("scroll");


// SCROLL TO TARGET 
// On click of the link, checks to see if the target exists
// If so, scrolls the page to that point, taking into account
// the back-to-top header

$("a[data-scroll-to-target]").on('click', function () {
    var target = $(this).data('scroll-to-target'),
        $target = $(target);
        headerOffset = $('.app-shortlist-banner__fixed').outerHeight() || 50;

    setTimeout(function() {
        if ($target.length > 0) {
            var scrollTo = $target.offset().top - headerOffset;
            $('html, body').animate({scrollTop: scrollTo}, 0);
        }
    }, 10)

});
