function notyMessageBox(type, text) {

    var n = noty({
        text: text,
        type: type,
        dismissQueue: true,
        progressBar: true,
        timeout: 5000,
        layout: 'topRight',
        closeWith: ['click'],
        theme: 'relax',
        maxVisible: 10,
        animation: {
            open: 'animated bounceInLeft',
            close: 'animated bounceOutLeft',
            easing: 'swing',
            speed: 500
        }
    });
    console.log('html: ' + n.options.id);
    return n;
}

