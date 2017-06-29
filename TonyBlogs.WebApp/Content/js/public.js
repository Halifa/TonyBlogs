function BeginWaitView(obj) {
    if (obj) {
        obj.block({
            message: $("#baseLoadingContent"),
            centerY: true,
            baseZ: 99999,
            css: {
                //top: '10%',
                border: 'none',
                padding: '2px',
                backgroundColor: 'none',
                cursor: "normal"
            },
            overlayCSS: {
                backgroundColor: '#ffffff',
                opacity: 0.3,
                cursor: "normal"
            }
        });
    } else {
        if ($("#baseLoadingContent").length > 0) {
            $.blockUI({
                message: $("#baseLoadingContent"),
                centerY: true,
                baseZ: 99999,
                css: {
                    //top: '10%',
                    border: 'none',
                    padding: '2px',
                    backgroundColor: 'none',
                    cursor: "normal"
                },
                overlayCSS: {
                    backgroundColor: '#ffffff',
                    opacity: 0.3,
                    cursor: "normal"
                }
            });
        } else {
            $.blockUI({
                message: "<img src='http://esf.js.soufunimg.com/esf/zueb/Public/assets/img/ajax-loading.gif'>",
                centerY: true,
                baseZ: 99999,
                css: {
                    //top: '10%',
                    border: 'none',
                    padding: '2px',
                    backgroundColor: 'none',
                    cursor: "normal"
                },
                overlayCSS: {
                    backgroundColor: '#ffffff',
                    opacity: 0.3,
                    cursor: "normal"
                }
            });
        }

    }
}

function EndWaitView(obj) {
    obj ? obj.unblock() : $.unblockUI();
}