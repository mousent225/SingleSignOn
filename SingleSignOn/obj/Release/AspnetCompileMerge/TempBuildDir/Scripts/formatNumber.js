(function ($) {

   
    $.fn.priceFormat = function (options) {

        var defaults =
		{
		    prefix: 'US$ ',
		    suffix: '',
		    centsSeparator: '.',
		    thousandsSeparator: ',',
		    limit: false,
		    centsLimit: 2,
		    clearPrefix: false,
		    clearSufix: false,
		    allowNegative: false,
		    insertPlusSign: false,
		    clearOnEmpty: false
		};

        var options = $.extend(defaults, options);

        return this.each(function () {
            var obj = $(this);
            var value = '';
            var is_number = /[0-9]/;

            if (obj.is('input'))
                value = obj.val();
            else
                value = obj.html();

            var prefix = options.prefix;
            var suffix = options.suffix;
            var centsSeparator = options.centsSeparator;
            var thousandsSeparator = options.thousandsSeparator;
            var limit = options.limit;
            var centsLimit = options.centsLimit;
            var clearPrefix = options.clearPrefix;
            var clearSuffix = options.clearSuffix;
            var allowNegative = options.allowNegative;
            var insertPlusSign = options.insertPlusSign;
            var clearOnEmpty = options.clearOnEmpty;

            if (insertPlusSign) allowNegative = true;

            function set(nvalue) {
                if (obj.is('input'))
                    obj.val(nvalue);
                else
                    obj.html(nvalue);
            }

            function get() {
                if (obj.is('input'))
                    value = obj.val();
                else
                    value = obj.html();

                return value;
            }

            function to_numbers(str) {
                var formatted = '';
                for (var i = 0; i < (str.length) ; i++) {
                    char_ = str.charAt(i);
                    if (formatted.length == 0 && char_ == 0) char_ = false;

                    if (char_ && char_.match(is_number)) {
                        if (limit) {
                            if (formatted.length < limit) formatted = formatted + char_;
                        }
                        else {
                            formatted = formatted + char_;
                        }
                    }
                }

                return formatted;
            }

            function fill_with_zeroes(str) {
                while (str.length < (centsLimit + 1)) str = '0' + str;
                return str;
            }

            function price_format(str, ignore) {
                if (!ignore && (str === '' || str == price_format('0', true)) && clearOnEmpty)
                    return '';

                var formatted = fill_with_zeroes(to_numbers(str));
                var thousandsFormatted = '';
                var thousandsCount = 0;

                if (centsLimit == 0) {
                    centsSeparator = "";
                    centsVal = "";
                }

                var centsVal = formatted.substr(formatted.length - centsLimit, centsLimit);
                var integerVal = formatted.substr(0, formatted.length - centsLimit);

                formatted = (centsLimit == 0) ? integerVal : integerVal + centsSeparator + centsVal;

                if (thousandsSeparator || $.trim(thousandsSeparator) != "") {
                    for (var j = integerVal.length; j > 0; j--) {
                        char_ = integerVal.substr(j - 1, 1);
                        thousandsCount++;
                        if (thousandsCount % 3 == 0) char_ = thousandsSeparator + char_;
                        thousandsFormatted = char_ + thousandsFormatted;
                    }

                    //
                    if (thousandsFormatted.substr(0, 1) == thousandsSeparator) thousandsFormatted = thousandsFormatted.substring(1, thousandsFormatted.length);
                    formatted = (centsLimit == 0) ? thousandsFormatted : thousandsFormatted + centsSeparator + centsVal;
                }

                if (allowNegative && (integerVal != 0 || centsVal != 0)) {
                    if (str.indexOf('-') != -1 && str.indexOf('+') < str.indexOf('-')) {
                        formatted = '-' + formatted;
                    }
                    else {
                        if (!insertPlusSign)
                            formatted = '' + formatted;
                        else
                            formatted = '+' + formatted;
                    }
                }

                if (prefix) formatted = prefix + formatted;

                if (suffix) formatted = formatted + suffix;

                return formatted;
            }

            function key_check(e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                var typed = String.fromCharCode(code);
                var functional = false;
                var str = value;
                var newValue = price_format(str + typed);

                if ((code >= 48 && code <= 57) || (code >= 96 && code <= 105)) functional = true;

                if (code == 8) functional = true;
                if (code == 9) functional = true;
                if (code == 13) functional = true;
                if (code == 46) functional = true;
                if (code == 37) functional = true;
                if (code == 39) functional = true;
                if (allowNegative && (code == 189 || code == 109 || code == 173)) functional = true;
                if (insertPlusSign && (code == 187 || code == 107 || code == 61)) functional = true;

                if (!functional) {

                    e.preventDefault();
                    e.stopPropagation();
                    if (str != newValue) set(newValue);
                }

            }

            function price_it() {
                var str = get();
                var price = price_format(str);
                if (str != price) set(price);
                if (parseFloat(str) == 0.0 && clearOnEmpty) set('');
            }

            function add_prefix() {
                obj.val(prefix + get());
            }

            function add_suffix() {
                obj.val(get() + suffix);
            }

            function clear_prefix() {
                if ($.trim(prefix) != '' && clearPrefix) {
                    var array = get().split(prefix);
                    set(array[1]);
                }
            }

            function clear_suffix() {
                if ($.trim(suffix) != '' && clearSuffix) {
                    var array = get().split(suffix);
                    set(array[0]);
                }
            }

            obj.bind('keydown.price_format', key_check);
            obj.bind('keyup.price_format', price_it);
            obj.bind('focusout.price_format', price_it);

            if (clearPrefix) {
                obj.bind('focusout.price_format', function () {
                    clear_prefix();
                });

                obj.bind('focusin.price_format', function () {
                    add_prefix();
                });
            }

            if (clearSuffix) {
                obj.bind('focusout.price_format', function () {
                    clear_suffix();
                });

                obj.bind('focusin.price_format', function () {
                    add_suffix();
                });
            }

            if (get().length > 0) {
                price_it();
                clear_prefix();
                clear_suffix();
            }

        });

    };

    $.fn.unpriceFormat = function () {
        return $(this).unbind(".price_format");
    };
    $.fn.unmask = function () {

        var field;
        var result = "";

        if ($(this).is('input'))
            field = $(this).val();
        else
            field = $(this).html();

        for (var f in field) {
            if (!isNaN(field[f]) || field[f] == "-") result += field[f];
        }

        return result;
    };

})(jQuery);