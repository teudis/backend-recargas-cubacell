$(document).ready(function () {

    $("#emailtarget").hide();
    $("#phonetarget").hide();

    $('input, select').change(function () {

        var selected = $("#perfilrecarga").val();
        switch (selected) {

            case "nauta": $("#emailtarget").show(); $("#phonetarget").hide(); break;
            case "cubacel": $("#phonetarget").show(); $("#emailtarget").hide(); break;

            default: $("#emailtarget").hide(); $("#phonetarget").hide();
        }
    });

    $('#FechaInicio').datetimepicker({
       
        locale: 'es'
    });

    $('#FechaFin').datetimepicker({
        
        locale: 'es'
    });


});