//(function ($, window) {
//    var arrowWidth = 30;
//    $.fn.resizeselect = function (settings) {
//        return this.each(function () {

//            $(this).change(function () {
//                var $this = $(this);

//                // crear elemento de prueba
//                var text = $this.find("option:selected").text();
//                var $test = $("<span>").html(text).css({
//                    "font-size": $this.css("font-size"), // asegura el mismo tamaño de texto
//                    "visibility": "hidden"               // previene el FOUC
//                });

//                // agregar al padre, obtener ancho y salir
//                $test.appendTo($this.parent());
//                var width = $test.width();
//                $test.remove();

//                // establecer ancho de selección
//                $this.width(width + arrowWidth);
//                $this.className = "blue";
//                // ejecutar al inicio
                
//                switch (text) {
//                    case "Municipio Total":
//                        $("select").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("#tabla").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("#chart").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("select").addClass("muntotal");
//                        $("#tabla").addClass("muntotal");
//                        $("#chart").addClass("muntotal");
//                        break;
//                    case "Adm. y Serv. Municipales":
//                        $("select").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("#tabla").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("#chart").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("select").addClass("admmunicipal");
//                        $("#tabla").addClass("admmunicipal");
//                        $("#chart").addClass("admmunicipal");
//                        break;
//                    case "Salud":
//                        $("select").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("#tabla").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("#chart").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("select").addClass("salud");
//                        $("#tabla").addClass("salud");
//                        $("#chart").addClass("salud");
//                        break;
//                    case "Educación":
//                        $("select").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("#tabla").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("#chart").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("select").addClass("educacion");
//                        $("#tabla").addClass("educacion");
//                        $("#chart").addClass("educacion");
//                        break;
//                    case "Cementerio":
//                        $("select").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("#tabla").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("#chart").removeClass("muntotal admmunicipal salud educacion cementerio");
//                        $("select").addClass("cementerio");
//                        $("#tabla").addClass("cementerio");
//                        $("#chart").addClass("cementerio");
//                        break;
//                }
                


//            }).change();

//        });
//    };
//    $("select.resizeselect").resizeselect();

    

//})(jQuery, window);


(function ($, window) {
    var arrowWidth = 25;
    $.fn.resizeselect = function (settings) {
        return this.each(function () {

            $(this).change(function () {
                var $this = $(this);

                // crear elemento de prueba
                var text = $this.find("option:selected").text();
                var font = $this.css("font-size");
                var tamano = getTextWidth(text, font);
                //$test.appendTo($this.parent());
                //var width = $test.width();
                //$test.remove();

                // establecer ancho de selección
                $this.width(tamano+1 + arrowWidth);
                $this.className = "blue";
                // ejecutar al inicio

                switch (text) {
                    case "Municipio Total":
                        $("select").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("#tabla").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("#chart").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("select").addClass("muntotal");
                        $("#tabla").addClass("muntotal");
                        $("#chart").addClass("muntotal");
                        break;
                    case "Adm. y Serv. Municipales":
                        $("select").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("#tabla").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("#chart").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("select").addClass("admmunicipal");
                        $("#tabla").addClass("admmunicipal");
                        $("#chart").addClass("admmunicipal");
                        break;
                    case "Salud":
                        $("select").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("#tabla").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("#chart").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("select").addClass("salud");
                        $("#tabla").addClass("salud");
                        $("#chart").addClass("salud");
                        break;
                    case "Educación":
                        $("select").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("#tabla").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("#chart").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("select").addClass("educacion");
                        $("#tabla").addClass("educacion");
                        $("#chart").addClass("educacion");
                        break;
                    case "Cementerio":
                        $("select").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("#tabla").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("#chart").removeClass("muntotal admmunicipal salud educacion cementerio");
                        $("select").addClass("cementerio");
                        $("#tabla").addClass("cementerio");
                        $("#chart").addClass("cementerio");
                        break;
                }



            }).change();

        });
    };
    $("select.resizeselect").resizeselect();



})(jQuery, window);

function puntos(aux) {
    aux = aux.toString().split('').reverse().join('').replace(/(?=\d*\.?)(\d{3})/g, '$1.');
    aux = aux.split('').reverse().join('').replace(/^[\.]/, '');
    return aux;
}

function redesclick(aux) {
    var url = "";
    switch (aux) {
        case 'twitter':
            url = "https://twitter.com/?status=";
            url = url + "Me gusta esta página " + window.location.href;
            break;
        case 'facebook':
            url = "https://www.facebook.com/sharer/sharer.php?u=";
            url = url + window.location.href;
            break;
    }
    windowObjectReference = window.open(
        url,
        "DescriptiveWindowName",
        "resizable,scrollbars,status"
    );
}


function getTextWidth(text, font) {
    // re-use canvas object for better performance
    var canvas = getTextWidth.canvas || (getTextWidth.canvas = document.createElement("canvas"));
    var context = canvas.getContext("2d");
    context.font = font +" Roboto";
    var metrics = context.measureText(text);
    return metrics.width;
}


function DescargaDataset(aux) {
    var IDANO = $("#ano").val();
    $.ajax({
        cache: false,
        async: false,
        type: 'POST',
        data: {
            year: IDANO,
            origenData: aux
        },
        url: "../../../../Admin/Home/Descarga",
        dataType: 'json',
        success: function (data) {
            console.log("data");
            console.log(data);
            abrirEnPestana(data);
        }
        , error: function (error) {
            console.log("error");
            console.log(error);
        }
    });
}
function abrirEnPestana(url) {
    var a = document.createElement("a");
    a.target = "_blank";
    a.href = url;
    a.click();
}
