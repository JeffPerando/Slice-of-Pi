﻿//this is the SITE-WIDE JS file
//if you want to add something to the index, use index.js

//please don't touch this import, ty
import '../lib/bootstrap/dist/js/bootstrap.bundle.js';

$(function () {
    //don't touch this either
    //I MEAN IT, it took me way too long to get this code working
    var popovers = [];
    $(".sp-popover").each(function (i) {
        popovers.push(new bootstrap.Popover(this, {
            trigger: 'hover focus'
        }));
    });

})
