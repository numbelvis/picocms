if (window['$']) {
    // Mikes change
    var picocms = {}
    picocms.App = {

        run: function () {

            var contentregions = $('[contentregion]');
            if (contentregions.length > 0)
                $.each(contentregions, function (pos, contentregion) {

                    picocms.App.attachOverlay(contentregion);
                });
        },

        attachOverlay: function (region) {

            var r = $(region),
                id = r.attr('contentregion'),
                pos = r.offset(),
                width = r.outerWidth(),
                height = r.outerHeight(),
                overlay = $('<div style="cursor:pointer;z-index:10001;position:absolute;font-weight: bold; padding: 3px 5px 3px 5px; background-color: LightBlue;"><span style="color:white;font-family:Arial;font-size: 11px; border: solid 1px white;padding:2px;">edit content: ' + id + '</span></div><div style="cursor:pointer;position:absolute;width:' + width + 'px;height:' + height + 'px; border: solid 1px LightBlue;background-color: LightBlue;filter:alpha(opacity=50);-moz-opacity:0.5;-khtml-opacity: 0.5;opacity: 0.5;z-index: 10000;">&nbsp;</div>');
            var content = r.text();
            
            overlay.hide();

            var showoverlay = false;
            r.mouseenter(function () {

                overlay.show();
            });

            overlay.mouseout(function () {

                overlay.hide();
            });

            var editor = $('<div style="z-index:10010;display:none;border:solid 1px orange;position:absolute;top:' + pos.top + 'px;left:' + pos.left + 'px;"></div>'),
                text = $('<textarea style="height: 200px;width:400px;" />'),
                save = $('<div style="background-color: green; color: white; font-weight: bold; font-family: Tahoma; padding: 3px 5px 3px 5px; width: 50%">save</div>'),
                cancel = $('<div style="float: right; background-color: red; color: white; font-weight: bold; font-family: Tahoma; padding: 3px 5px 3px 5px; width: 50%;">cancel</div>'),
                toolbar = $('<div style="cursor:pointer;"></div>');

            toolbar.append(cancel);
            toolbar.append(save);

            editor.append(text);
            editor.append(toolbar);

            function reattach() {

                overlay.click(function () {

                    if (content) content = $.trim(content);
                    text.val(content);
                    editor.show();
                    text.focus();
                });

                overlay.insertBefore(r);
                r.append(editor);

                cancel.click(function () {

                    editor.hide();
                    overlay.hide();
                });

                save.click(function () {

                    content = text.val();
                    r.html(content);

                    picocms.App.save(id, content, function (result) {

                        editor.hide();
                        overlay.hide();
                        reattach();
                    });
                });
            }

            reattach();
        },

        save: function (contentid, html, callback) {

            $.ajax({
                type: "POST",
                url: "/picocms.ashx",
                data: "contentregion=" + contentid + "&html=" + html,
                success: function (msg) {
                    callback();
                },
                failure: function (msg) {

                    alert('Saving the content failed, sorry!');
                }
            });
        }
    }

    $(document).ready(function () {
        picocms.App.run();
    });
}
else {

    if (console && console.log)
        console.log('picocms requires jQuery!  Add it to your document <head>');
}