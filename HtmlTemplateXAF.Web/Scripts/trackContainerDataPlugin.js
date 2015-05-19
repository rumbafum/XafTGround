
(function ($) {

    $.fn.trackControls = function (options) {

        var formData = {HTML: "", Controls: []};

        var settings = $.extend({
            loadDefaultValues: false,
            valueByControlType: true,
            formDataId: 'post_data',
            appendToForm: true,
            formIdToAppend: 'form1',
            validateControlOnChange: true
        }, options);

        var plugin = this;

        var getUniqueIdentifier = function (control) {
            var identifier = $(control).attr("id");
            if (identifier != undefined)
                return identifier;
            identifier = $(control).attr("name");
            if (identifier != undefined)
                return identifier;
            return $(control).attr("class");
        }

        var getValueByControl = function (control) {
            if (!settings.valueByControlType)
                return $(control).val();

            switch ($(control).prop('type')) {
                case 'checkbox':
                    return $(control).is(":checked");
                default:
                    return $(control).val();
            }
        }

        var setValueByControl = function (control, value) {
            switch ($(control).prop('type')) {
                case 'checkbox':
                    if (value)
                        $(control).attr("checked", "checked");
                    else
                        $(control).removeAttr("checked");
                    break;
                case 'select-one':
                    $(control).find("option").each(function () {
                        if ($(this).val() == value) {
                            $(this).attr("selected", "selected");
                        }
                        else
                            $(this).removeAttr('selected');
                    });
                    break;
                default:
                    $(control).attr("value", value);
                    break;
            }
        }

        var validateInternal = function (validationRule, control) {
            var controlValue = getValueByControl($(control));
            switch (validationRule.name) {
                case "mandatory":
                    if (controlValue == "" || controlValue == false)
                        return "Field is mandatory";
                    break;
                case "regexp":
                    var re = new RegExp(validationRule.value, 'g');
                    if (!re.test(controlValue))
                        return "Invalid field value";
                    break;
                case "maxlength":
                    if (controlValue.length > validationRule.value)
                        return "Field max length exceeded";
                    break;
                default:
                    break;
            }
            return "";
        }

        var getValidationAttributeList = function (control, attributes) {
            var result = [];
            $.each($(control)[0].attributes, function () {
                if (this.specified) {
                    if (_.contains(attributes, this.name))
                        result.push({"name": this.name, "value": this.value});
                }
            });
            return result;
        }

        var checkValidationRules = function (control) {
            var validationRules = getValidationAttributeList(control, ["mandatory", "maxlength", "regexp"]);
            for (var i = 0; i < validationRules.length; i++) {
                var msg = validateInternal(validationRules[i], control);
                if (msg != "") return msg;
            }
            return "";
        }

        /*Public*/

        plugin.getFormData = function () {
            formData.HTML = $(plugin).prop("outerHTML");
            if (settings.validateControlOnChange)
                return JSON.stringify(formData);
            $(this).find(':input').each(function () {
                var errorMsg = checkValidationRules($(this));
                var savedElem = _.findWhere(formData.Controls, { control: getUniqueIdentifier($(this)) });
                if (savedElem != undefined) {
                    savedElem.value = getValueByControl($(this));
                    savedElem.error = errorMsg;
                }
                else {
                    var fieldName = $(this).attr("fieldName");
                    formData.Controls.push({ control: getUniqueIdentifier($(this)), value: getValueByControl($(this)), fieldName: fieldName == undefined ? "": fieldName, error: errorMsg });
                }
            });
            return JSON.stringify(formData);
        }

        /*End Public*/

        $(this).find(':input').each(function () {

            if (settings.loadDefaultValues) {
                var errorMsg = checkValidationRules($(this));
                $(this).parent().find("span").remove();
                if (errorMsg != "") {
                    $(this).parent().append("<span>*</span>").attr("title", errorMsg);
                }
                var fieldName = $(this).attr("fieldName");
                formData.Controls.push({ control: getUniqueIdentifier($(this)), value: getValueByControl($(this)), fieldName: fieldName == undefined ? "" : fieldName, error: errorMsg });
                var html = $(plugin).prop("outerHTML");
                formData.HTML = html;
                $(".formData").val(JSON.stringify(formData));
            }

            if (!settings.validateControlOnChange)
                return;

            $(this).on("change keyup paste", function () {

                var errorMsg = checkValidationRules($(this));

                $(this).parent().find("span").remove();
                if (errorMsg != "") {
                    $(this).parent().append("<span>*</span>").attr("title", errorMsg);
                }
                var savedElem = _.findWhere(formData.Controls, { control: getUniqueIdentifier($(this)) });
                if (savedElem != undefined) {
                    savedElem.value = getValueByControl($(this));
                    savedElem.error = errorMsg;
                }
                else {
                    var fieldName = $(this).attr("fieldName")
                    formData.Controls.push({ control: getUniqueIdentifier($(this)), value: getValueByControl($(this)), fieldName: fieldName == undefined ? "" : fieldName, error: errorMsg });
                }

                setValueByControl($(this), getValueByControl($(this)));
                var html = $(plugin).prop("outerHTML");
                formData.HTML = html;

                $(".formData").val(JSON.stringify(formData));
            });
        });

        if (settings.appendToForm) {
            $("#" + settings.formIdToAppend).submit(function (event) {
                var input = $("<input>").attr("type", "hidden").attr("name", settings.formDataId).val(plugin.getFormData());
                $("#" + settings.formIdToAppend).append($(input));
            });
        }

        return this;

    };

}(jQuery));