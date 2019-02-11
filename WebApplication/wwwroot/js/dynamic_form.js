 $(document).ready(function () {

            var rowcubacel = 0;
            var rownauta = 0;
            var select_perfil_nauta = $("<select class='form-control' id='perfil_nauta'></select>");
            var select_perfil_cubacel = $("<select class='form-control' id='perfil_cubacel'></select>");
            $("#button_send").hide();

            //Generar campos cubacel
            $(document).on('click', '#id_cubalcel', function (e) {
                e.preventDefault();
                $("#button_send").show();
                rowcubacel++;
                var cubacel = '<div id="rowNum' + rowcubacel + '"><h3>Recarga Cubacel</h3><label for="numero_cell" class="col-sm-2 control-label">N&uacute;mero Movil</label><div class="col-sm-4"><input type="text" class="form-control" id="cubacel' + rowcubacel + '"></div><label class="col-sm-3 control-label">Perfil Cubacel</label><div class="col-sm-3" id="select_cubacel' + rowcubacel + '"></div></div><br>';
                jQuery('#dynamic_inputs').append(cubacel);
                var copy = select_perfil_cubacel.clone();
                $("#select_cubacel" + rowcubacel).append(copy);  
            });

             //Generar campos nauta
            $(document).on('click', '#nauta', function (e) {
                e.preventDefault();
                $("#button_send").show();
                rownauta++;
                var nauta = '<div id="rowNum' + rownauta + '"><h3>Recarga Nauta</h3><label for="numero_cell" class="col-sm-2 control-label">N&uacute;mero Casa</label><div class="col-sm-4"><input type="text" class="form-control" id="nauta' + rownauta + '"></div><label class="col-sm-3 control-label">Perfil Nauta</label><div class="col-sm-3" id="select_nauta' + rownauta + '"></div></div><br>';
                jQuery('#dynamic_inputs').append(nauta); 
                var copy = select_perfil_nauta.clone();
                $("#select_nauta" + rownauta).append(copy);               

            });

            // get perfil nauta
            $(function () {
                $.get('/account/recarga/getperfilnauta').done(function (perfil_nauta) {
                    $.each(perfil_nauta, function (i, nauta) {

                        var opt = $("<option></option");
                        opt.val(nauta.id); opt.html(nauta.label);                        
                        select_perfil_nauta.append(opt);
                        select_perfil_nauta.val(opt.val());
                    });
                    
                });
            }); 

            // get perfil cubacel
            $(function () {
                $.get('/account/recarga/getperfilcubacel').done(function (perfil_cubacel) {
                    $.each(perfil_cubacel, function (i, cubacel) {

                        var opt = $("<option></option");
                        opt.val(cubacel.id); opt.html(cubacel.label);
                        select_perfil_cubacel.append(opt);
                        select_perfil_cubacel.val(opt.val());
                    });

                });
            });

        });