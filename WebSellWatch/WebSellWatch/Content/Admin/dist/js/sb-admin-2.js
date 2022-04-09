/*!
 * Start Bootstrap - SB Admin 2 v3.3.7+1 (http://startbootstrap.com/template-overviews/sb-admin-2)
 * Copyright 2013-2016 Start Bootstrap
 * Licensed under MIT (https://github.com/BlackrockDigital/startbootstrap/blob/gh-pages/LICENSE)
 */
let signin_form = document.querySelector('#signin-form')

let signin_btn = document.querySelector('#form-control btn btn-success')

let btnfb = document.querySelector('#btnfb')

validateEmail = (email) => {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/

    return re.test(String(email).toLowerCase())
}

checkSigninInput = (input) => {
    let err_span = signin_form.querySelector(`span[data-err-for="${input.id}"]`)
    let val = input.value.trim()
    let form_group = input.parentElement

    switch (input.getAttribute('type')) {
        case 'password':
            if (val.length < 6) {
                form_group.classList.add('err')
                form_group.classList.remove('success')
                err_span.innerHTML = 'Password must be at least 6 characters'
            } else {
                form_group.classList.add('success')
                form_group.classList.remove('err')
                err_span.innerHTML = ''
            }
            break;
        case 'email':
            if (val.length === 0 || !validateEmail(val)) {
                form_group.classList.add('err')
                form_group.classList.remove('success')
                err_span.innerHTML = 'Email is invalid'
            } else {
                form_group.classList.add('success')
                form_group.classList.remove('err')
                err_span.innerHTML = ''
            }
    }
}

checkSigninForm = () => {
    let inputs = signin_form.querySelectorAll('.form-control')
    inputs.forEach(input => checkSigninInput(input))
}

signin_btn.onclick = () => {
    checkSigninForm()
}

let inputs = signin_form.querySelectorAll('.form-control')
inputs.forEach(input => {
    input.addEventListener('focusout', () => {
        checkSigninInput(input)
    })
})
$(function() {
    $('#side-menu').metisMenu();
});

//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
// Sets the min-height of #page-wrapper to window size
$(function() {
    $(window).bind("load resize", function() {
        var topOffset = 50;
        var width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            $('div.navbar-collapse').addClass('collapse');
            topOffset = 100; // 2-row-menu
        } else {
            $('div.navbar-collapse').removeClass('collapse');
        }

        var height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
        height = height - topOffset;
        if (height < 1) height = 1;
        if (height > topOffset) {
            $("#page-wrapper").css("min-height", (height) + "px");
        }
    });

    var url = window.location;
    // var element = $('ul.nav a').filter(function() {
    //     return this.href == url;
    // }).addClass('active').parent().parent().addClass('in').parent();
    var element = $('ul.nav a').filter(function() {
        return this.href == url;
    }).addClass('active').parent();

    while (true) {
        if (element.is('li')) {
            element = element.parent().addClass('in').parent();
        } else {
            break;
        }
    }
});
