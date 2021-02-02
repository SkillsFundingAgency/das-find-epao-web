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
        placeholder: '',
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
    var isIE11 = !!window.MSInputMethodContext && !!document.documentMode;
    var referrerHost;

    var homeLink = $('<div class="govuk-breadcrumbs"><ol class="govuk-breadcrumbs__list"><li class="govuk-breadcrumbs__list-item"><a href="/" class="govuk-breadcrumbs__link">Home</a></li></ol></div>');

    if (!isIE11 && referrer) {
        referrerHost = new URL(referrer).hostname;
    }

    if (!referrer || referrerHost && referrerHost !== document.location.hostname) {
        $backLinkOrHome.replaceWith(homeLink);
    }
}

if ($backLinkOrHome) {
    backLinkOrHome();
}
