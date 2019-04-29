$(document).ready(function () {

    var rowcubacel = 0;
    var rownauta = 0;
    var datos_validos = 0;
    var select_perfil_nauta = $("<select class='form-control' id='perfil_nauta'></select>");
    var select_perfil_cubacel = $("<select class='form-control' id='perfil_cubacel'></select>");
    var arr_cubacel = [];
    var arr_nauta = [];

    // Cubacel TuneUps Submit
    $(document).on('click', '#button_send_cubacel', function (e) {
        for (var i = 1; i <= rowcubacel; i++) {
            var number = $("#cubacel" + i).val().trim();

            if (!ValidarCubacel(number)) {

                $("#cubacel" + i).attr('style', "border-radius: 5px; border:#FF0000 1px solid;");

            }
            else {

                $("#cubacel" + i).attr('style', "border-radius: 5px; border: #00B33C 1px solid;");
                datos_validos++;
            }
        }

        if (datos_validos == rowcubacel) {

            GetDatosCubacel();
            // procesar celular recargas
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/account/tuneup/requestcubaceltuneup",
                data: JSON.stringify(arr_cubacel),
                dataType: "json",
                success: function (response) {
                    alert(response.someValue);
                    var url = "/account/tuneup/cubacel";
                    window.location.href = url;
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
    });

    // Nauta TuneUps Submit
    $(document).on('click', '#button_send_nauta', function (e) {
        for (var i = 1; i <= rownauta; i++) {
            var number = $("#nauta" + i).val().trim();

            if (!ValidarNauta(number)) {
                $("#nauta" + i).attr('style', "border-radius: 5px; border:#FF0000 1px solid;");
            }
            else {
                $("#nauta" + i).attr('style', "border-radius: 5px; border: #00B33C 1px solid;");

                datos_validos++;
            }
        }

        if (datos_validos == rownauta) {
            GetDatosNauta();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/account/tuneup/requestnautatuneup",
                data: JSON.stringify(arr_nauta),
                dataType: "json",
                success: function (response) {
                    alert(response.someValue);
                    var url = "/account/tuneup/nauta";
                    window.location.href = url;
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
    });

    function ValidarNauta(correo) {

        var expReg = new RegExp(/^([a-z0-9_\.-]+)@nauta\.com.cu$/)

        if (expReg.test(correo)) {

            return true;
        }
        else
            return false;
    }

    function GetDatosCubacel() {
        cont_cubacel = 1;
        $('select#perfil_cubacel').each(function () {
            var perfil_cell = $(this).val().trim();
            var number_cell = $("#cubacel" + cont_cubacel).val().trim();
            arr_cubacel.push({ phonenumbertarget: number_cell, id: perfil_cell });
            
            cont_cubacel++
        });

    }

    function GetDatosNauta() {
        cont_nauta = 1;
        $('select#perfil_nauta').each(function () {
            var perfil_nauta = $(this).val().trim();
            var number_nauta = $("#nauta" + cont_nauta).val().trim();

            arr_nauta.push({ emailaddresstarget: number_nauta, id: perfil_nauta });
            cont_nauta++
        });

    }

    function ValidarCubacel(number) {

        var expReg = new RegExp(/^([52-58])+[0-9]{6}$/)
        if (expReg.test(number)) {

            return true;
        }
        else
            return false;
    }

    //Generar campos Cubacel
    $(document).on('click', '#add_cubalcel_tuneup_form', function (e) {
        e.preventDefault();

        $('#button_send_cubacel').removeClass('collapse');

        rowcubacel++;

        var cubacel = '<div id="rowNumCubacel' + rowcubacel + '"><div class="formulario_recargas-row"><div class="col-sm-4"><label for="numero_cell" class="control-label">N&uacute;mero de tel&eacute;fono</label><input type="text" class="form-control" id="cubacel' + rowcubacel + '"></div><div class="col-sm-4" id="select_cubacel' + rowcubacel + '"><label class="control-label">Perfil Cubacel</label></div><div class="col-sm-4"><button type="button" class="btn btn-danger">Cancelar esta Recarga</button></div></div></div>';

        $('#zone_cell').append(cubacel);

        var copy = select_perfil_cubacel.clone();

        $("#select_cubacel" + rowcubacel).append(copy);
    });


    //Generar campos Nauta
    $(document).on('click', '#add_nauta_tuneup_form', function (e) {
        e.preventDefault();

        $('#button_send_nauta').removeClass('collapse');

        rownauta++;

        var nauta = '<div id="rowNumNauta' + rownauta + '"><div class="formulario_recargas-row"><div class="col-sm-4"><label for="numero_cell" class="control-label">Usuario Nauta</label><input type="text" class="form-control" id="nauta' + rownauta + '"></div><div class="col-sm-4" id="select_nauta' + rownauta + '"><label class="control-label">Perfil Nauta</label></div><div class="col-sm-4"><button type="button" class="btn btn-danger">Cancelar esta Recarga</button></div></div></div>';

        $('#zone_nauta').append(nauta);

        var copy = select_perfil_nauta.clone();

        $("#select_nauta" + rownauta).append(copy);
    });

    // get perfil nauta
    $(function () {
        $.get('/account/tuneup/nautatuneupprofiles').done(function (perfil_nauta) {
            $.each(perfil_nauta, function (i, nauta) {

                var opt = $("<option></option");
                opt.val(nauta.id); opt.html(nauta.label + ' ($' + nauta.amount + ')');
                select_perfil_nauta.append(opt);
                select_perfil_nauta.val(opt.val());
            });

            $('#add_nauta_tuneup_form').removeClass('disabled');
        });
    });

    // get perfil cubacel
    $(function () {
        $.get('/account/tuneup/cubaceltuneupprofiles').done(function (perfil_cubacel) {
            $.each(perfil_cubacel, function (i, cubacel) {

                var opt = $("<option></option");

                opt.val(cubacel.id); opt.html(cubacel.label + ' ($' + cubacel.amount + ')');

                select_perfil_cubacel.append(opt);
                select_perfil_cubacel.val(opt.val());
            });

            $('#add_cubacel_tuneup_form').removeClass('disabled');
        });
    });

});