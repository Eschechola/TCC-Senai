
//NAVBAR STYLE

jQuery(document).ready(function ($) {
    var nav = document.getElementById('navbar');
    var collapse = document.getElementById('navbarSupportedContent');
    $(document).scroll(function (e) {
        var scrollTop = $(document).scrollTop();

        if (screen.width > 1024) {
            if (scrollTop > 0) {

                nav.style.height = "75px";

                nav.classList.add("navbar-style-shadow");

                if (nav.classList.contains("navbar-style-dark")) {
                    nav.style.backgroundColor = "#212121";
                }

            }
            else {
                nav.classList.remove("navbar-style-shadow");
                nav.style.height = "80px";
                if (nav.classList.contains("navbar-style-dark")) {
                    nav.style.backgroundColor = "transparent";
                }
            }
        }

        if (scrollTop > 0) {
            if (nav.classList.contains("navbar-style-dark")) {
                nav.style.backgroundColor = "#212121";
            }
            nav.classList.add("navbar-style-shadow");
        }
        else {
            nav.classList.remove("navbar-style-shadow");
            if (nav.classList.contains("navbar-style-dark")) {

                if (!collapse.classList.contains("show")) {
                    nav.style.backgroundColor = "transparent";
                }
            }
        }

    });
});

function makeNavbarBlack() {
    var nav = document.getElementById('navbar');

    var scrollTop = $(document).scrollTop();
    if (scrollTop <= 0) {
        if (window.getComputedStyle(nav, null).getPropertyValue("background-color") === "rgba(0, 0, 0, 0)") {
            nav.style.backgroundColor = "#212121";
        }
        else {
            nav.style.backgroundColor = "transparent";
        }
    }


}

var btnEnviar = document.getElementById("Enviar");
var inpPesquisa = document.getElementById("Pesquisa");
var formPesquisar = document.getElementById("form-pesquisar");

if (btnEnviar) {
    btnEnviar.addEventListener('pointerdown', function () {
        event.preventDefault();
        if (inpPesquisa.classList.length > 0) {
            var nome = $("#Pesquisa").val();
            if (nome == "") {
                return;
            }
            else {

            document.location.href = '/Home/Pesquisa/' + nome;
            }
        }
        else if (inpPesquisa.classList.length == 0) {
            inpPesquisa.classList.add("Pesquisa-Show");
            inpPesquisa.focus();
        }
    });
}

if (inpPesquisa) {
    inpPesquisa.addEventListener('blur', function () {

        inpPesquisa.classList.remove("Pesquisa-Show");

    });
}

window.onload = function () {

    this.animateScroll();
    var inputs = document.querySelectorAll(".input-style");

    if (inputs !== null) {
        inputs.forEach(function (input) {
            input.addEventListener("focus", function () { changeBorder(input); });
            input.addEventListener("blur", function () { resetBorder(input); });
        });
    }
};

function changeBorder(inputs) {
    inputs.style.borderColor = "#436477";
    inputs.style.borderBottomWidth = "1.2px";
}

function resetBorder(inputs) {
    inputs.style.borderColor = "#b4b4b4";
    inputs.style.borderBottomWidth = "1px";
}

var offer = document.getElementById("offer");
var line = document.getElementById("line-index-1");

var responsive_text1 = document.getElementById("responsive-text-1");
var image_devices1 = document.getElementById("responsive-image-1");

var responsive_text2 = document.getElementById("responsive-text-2");
var image_devices2 = document.getElementById("responsive-image-2");
var image_devices2mb = document.getElementById("responsive-image-2mob");

var responsive_text3 = document.getElementById("responsive-text-3");
var image_devices3 = document.getElementById("responsive-image-3");

var responsive_text4 = document.getElementById("responsive-text-4");
var image_devices4 = document.getElementById("responsive-image-4");
var image_devices4mb = document.getElementById("responsive-image-4mob");

var sessao3 = document.getElementById("sessao3");

var sessao4 = document.getElementById("sessao4");

var numberAlreadyDone = false;

function animate_h1() {

    var title_h1 = document.getElementById("title-sessao1");

    var img_fundo = document.getElementById("img-fundo");

    if (img_fundo !== null && title_h1 !== null) {
        img_fundo.style.filter = "blur(3px) grayscale(50%)";

        if (document.width < 768) {
            title_h1.style.position = "relative";
            title_h1.style.opacity = "1";
            title_h1.style.textAlign = "center";
        }
        else {
            title_h1.style.left = "50%";
            title_h1.style.opacity = "1";
        }
    }

    setTimeout(animate_h2, 1200);
}

function animateScroll() {

    animate_h1();

    var scrollTop = $(document).scrollTop();
    var nav = document.getElementById('navbar');
    var collapse = document.getElementById('navbarSupportedContent');

    if (screen.width > 1024) {
        if (scrollTop > 0) {
            nav.style.height = "75px";

            nav.classList.add("navbar-style-shadow");

            if (nav.classList.contains("navbar-style-dark")) {
                nav.style.backgroundColor = "#212121";
            }

        }
        else {
            nav.classList.remove("navbar-style-shadow");
            nav.style.height = "80px";
            if (nav.classList.contains("navbar-style-dark")) {
                nav.style.backgroundColor = "transparent";
            }
        }
    }

    if (scrollTop > 0) {
        if (nav.classList.contains("navbar-style-dark")) {
            nav.style.backgroundColor = "#212121";
        }
        nav.classList.add("navbar-style-shadow");
    }
    else {
        nav.classList.remove("navbar-style-shadow");
        if (nav.classList.contains("navbar-style-dark")) {

            if (!collapse.classList.contains("show")) {
                nav.style.backgroundColor = "transparent";
            }
        }
    }

    if (offer !== null) {


        if (scrollTop > offer.offsetTop / 3.5) {
            offer.style.paddingTop = "0px";
            offer.style.opacity = "1";
            line.style.width = "30%";
        } else {
            offer.style.paddingTop = "300px";
            offer.style.opacity = "0";
            line.style.width = "100%";
        }

        if (scrollTop > responsive_text1.offsetTop / 2) {
            responsive_text1.style.paddingRight = "0px";
            responsive_text1.style.opacity = "1";
            image_devices1.style.paddingLeft = "0px";
            image_devices1.style.opacity = "1";
        } else {
            responsive_text1.style.paddingRight = "300px";
            responsive_text1.style.opacity = "0";
            image_devices1.style.paddingLeft = "300px";
            image_devices1.style.opacity = "0";
        }

        if (scrollTop > responsive_text2.offsetTop / 2) {
            responsive_text2.style.paddingLeft = "0px";
            responsive_text2.style.opacity = "1";
            image_devices2.style.paddingRight = "0px";
            image_devices2.style.opacity = "1";
            image_devices2mb.style.opacity = "1";
            image_devices2mb.style.opacity = "1";
        } else {
            responsive_text2.style.paddingLeft = "300px";
            responsive_text2.style.opacity = "0";
            image_devices2.style.paddingRight = "300px";
            image_devices2.style.opacity = "0";
            image_devices2mb.style.paddingRight = "300px";
            image_devices2mb.style.opacity = "0";
        }

        if (scrollTop > responsive_text3.offsetTop / 2) {
            responsive_text3.style.paddingRight = "0px";
            responsive_text3.style.opacity = "1";
            image_devices3.style.paddingLeft = "0px";
            image_devices3.style.opacity = "1";
        } else {
            responsive_text3.style.paddingRight = "300px";
            responsive_text3.style.opacity = "0";
            image_devices3.style.paddingLeft = "300px";
            image_devices3.style.opacity = "0";
        }

        if (scrollTop > responsive_text4.offsetTop / 2) {
            responsive_text4.style.paddingLeft = "0px";
            responsive_text4.style.opacity = "1";
            image_devices4.style.paddingRight = "0px";
            image_devices4.style.opacity = "1";
            image_devices4mb.style.paddingRight = "0px";
            image_devices4mb.style.opacity = "1";
        } else {
            responsive_text4.style.paddingLeft = "300px";
            responsive_text4.style.opacity = "0";
            image_devices4.style.paddingRight = "300px";
            image_devices4.style.opacity = "0";
            image_devices4mb.style.paddingRight = "300px";
            image_devices4mb.style.opacity = "0";
        }

        if (scrollTop > sessao3.offsetTop / 1.30) {

            sessao3.style.width = "100%";
            sessao3.style.transform = "translateX(0%)";
            sessao3.style.opacity = "1";
        }
        else {
            sessao3.style.transform = "translateX(-140%)";
            hideSession3();
            sessao3.style.opacity = "0";
        }

        if (scrollTop > sessao4.offsetTop) {
            if (numberAlreadyDone === false) {
                $('.count').each(function () {
                    $(this).prop('Counter', 0).animate({
                        Counter: $(this).text()
                    }, {
                        duration: 3000,
                        easing: 'swing',
                        step: function (now) {
                            $(this).text(Math.ceil(now));
                        }
                    });
                });
                numberAlreadyDone = true;
            }
        }
    }

}

function animate_h2() {
    var title_h2 = document.getElementById("subtitle-sessao1");

    if (title_h2 !== null) {
        title_h2.style.left = "50%";
        title_h2.style.opacity = "1";
    }


    setTimeout(animate_button_init, 1200);
}

function animate_button_init() {
    var buttonIndex = document.getElementById("botao_iniciar");

    if (buttonIndex !== null) {
        buttonIndex.style.top = "80%";
        buttonIndex.style.opacity = "1";
    }

}

$(document).scroll(function () {

    var scrollTop = $(document).scrollTop();

    if (offer !== null) {
        if (scrollTop > offer.offsetTop / 3.5) {
            offer.style.paddingTop = "0px";
            offer.style.opacity = "1";
            line.style.width = "30%";
        } else {
            offer.style.paddingTop = "300px";
            offer.style.opacity = "0";
            line.style.width = "100%";
        }

        if (scrollTop > responsive_text1.offsetTop / 2) {
            responsive_text1.style.paddingRight = "0px";
            responsive_text1.style.opacity = "1";
            image_devices1.style.paddingLeft = "0px";
            image_devices1.style.opacity = "1";
        } else {
            responsive_text1.style.paddingRight = "300px";
            responsive_text1.style.opacity = "0";
            image_devices1.style.paddingLeft = "300px";
            image_devices1.style.opacity = "0";
        }

        if (scrollTop > responsive_text2.offsetTop / 1.5) {
            responsive_text2.style.paddingLeft = "0px";
            responsive_text2.style.opacity = "1";
            image_devices2.style.paddingRight = "0px";
            image_devices2.style.opacity = "1";
            image_devices2mb.style.paddingRight = "0px";
            image_devices2mb.style.opacity = "1";
        } else {
            responsive_text2.style.paddingLeft = "300px";
            responsive_text2.style.opacity = "0";
            image_devices2.style.paddingRight = "300px";
            image_devices2.style.opacity = "0";
            image_devices2mb.style.paddingRight = "300px";
            image_devices2mb.style.opacity = "0";
        }

        if (scrollTop > responsive_text3.offsetTop / 1.4) {
            responsive_text3.style.paddingRight = "0px";
            responsive_text3.style.opacity = "1";
            image_devices3.style.paddingLeft = "0px";
            image_devices3.style.opacity = "1";
        } else {
            responsive_text3.style.paddingRight = "300px";
            responsive_text3.style.opacity = "0";
            image_devices3.style.paddingLeft = "300px";
            image_devices3.style.opacity = "0";
        }

        if (scrollTop > responsive_text4.offsetTop / 1.4) {
            responsive_text4.style.paddingLeft = "0px";
            responsive_text4.style.opacity = "1";
            image_devices4.style.paddingRight = "0px";
            image_devices4.style.opacity = "1";
            image_devices4mb.style.paddingRight = "0px";
            image_devices4mb.style.opacity = "1";
        } else {
            responsive_text4.style.paddingLeft = "300px";
            responsive_text4.style.opacity = "0";
            image_devices4.style.paddingRight = "300px";
            image_devices4.style.opacity = "0";
            image_devices4mb.style.paddingRight = "300px";
            image_devices4mb.style.opacity = "0";
        }

        if (scrollTop > sessao3.offsetTop / 1.30) {
            
            sessao3.style.width = "100%";
            sessao3.style.transform = "translateX(0%)";
            sessao3.style.opacity = "1";
        }
        else {
            sessao3.style.transform = "translateX(-140%)";
            hideSession3();
            sessao3.style.opacity = "0";
        }

        if (scrollTop > sessao4.offsetTop / 1.3) {
            if (numberAlreadyDone === false) {
                $('.count').each(function () {
                    $(this).prop('Counter', 0).animate({
                        Counter: $(this).text()
                    }, {
                        duration: 3000,
                        easing: 'swing',
                        step: function (now) {
                            $(this).text(Math.ceil(now));
                        }
                    });
                });
                numberAlreadyDone = true;
            }
        }


    }

});

function hideSession3() {
    sessao3.style.width = "0px";
}

var visaoGeral = document.getElementById("visao-geral");
var editarPerfil = document.getElementById("editar-perfil");
var editarSenha = document.getElementById("editar-senha");
var historico = document.getElementById("historico");
var sugestao = document.getElementById("sugestao");

var divVisaoGeral = document.getElementById("conta-visao-geral");
var divEditarPerfil = document.getElementById("conta-editar-perfil");
var divEditarSenha = document.getElementById("conta-editar-senha");
var divHistorico = document.getElementById("conta-historico");
var divSugestao = document.getElementById("conta-sugestao");

if (visaoGeral) {
    visaoGeral.addEventListener('click', function () {
        event.preventDefault();

        if (screen.width > 768) {
            visaoGeral.style.borderLeft = "5px solid #083990";
            editarPerfil.style.borderLeft = "none";
            editarSenha.style.borderLeft = "none";
            historico.style.borderLeft = "none";
            sugestao.style.borderLeft = "none";
        }
        else {
            visaoGeral.style.borderTop = "5px solid #083990";
            editarPerfil.style.borderTop = "none";
            editarSenha.style.borderTop = "none";
            historico.style.borderTop = "none";
            sugestao.style.borderTop = "none";
        }

        divVisaoGeral.style.opacity = "1";
        divVisaoGeral.style.height = "auto";

        divEditarPerfil.style.opacity = "0";
        divEditarPerfil.style.height = "0px";

        divEditarSenha.style.opacity = "0";
        divEditarSenha.style.height = "0px";

        divHistorico.style.opacity = "0";
        divHistorico.style.height = "0px";

        divSugestao.style.opacity = "0";
        divSugestao.style.height = "0px";

    });
}

if (editarPerfil) {
    editarPerfil.addEventListener('click', function () {
        event.preventDefault();

        if (screen.width > 768) {
            visaoGeral.style.borderLeft = "none";
            editarPerfil.style.borderLeft = "5px solid #083990";
            editarSenha.style.borderLeft = "none";
            historico.style.borderLeft = "none";
            sugestao.style.borderLeft = "none";
        }
        else {
            visaoGeral.style.borderTop = "none";
            editarPerfil.style.borderTop = "5px solid #083990";
            editarSenha.style.borderTop = "none";
            historico.style.borderTop = "none";
            sugestao.style.borderTop = "none";
        }

        divVisaoGeral.style.opacity = "0";
        divVisaoGeral.style.height = "0px";

        divEditarPerfil.style.opacity = "1";
        divEditarPerfil.style.height = "auto";

        divEditarSenha.style.opacity = "0";
        divEditarSenha.style.height = "0px";

        divHistorico.style.opacity = "0";
        divHistorico.style.height = "0px";

        divSugestao.style.opacity = "0";
        divSugestao.style.height = "0px";
    });
}

if (editarSenha) {
    editarSenha.addEventListener('click', function () {
        event.preventDefault();

        if (screen.width > 768) {
            visaoGeral.style.borderLeft = "none";
            editarPerfil.style.borderLeft = "none";
            editarSenha.style.borderLeft = "5px solid #083990";
            historico.style.borderLeft = "none";
            sugestao.style.borderLeft = "none";
        }
        else {
            visaoGeral.style.borderTop = "none";
            editarPerfil.style.borderTop = "none";
            editarSenha.style.borderTop = "5px solid #083990";
            historico.style.borderTop = "none";
            sugestao.style.borderTop = "none";
        }

        divVisaoGeral.style.opacity = "0";
        divVisaoGeral.style.height = "0px";

        divEditarPerfil.style.opacity = "0";
        divEditarPerfil.style.height = "0px";

        divEditarSenha.style.opacity = "1";
        divEditarSenha.style.height = "auto";

        divHistorico.style.opacity = "0";
        divHistorico.style.height = "0px";

        divSugestao.style.opacity = "0";
        divSugestao.style.height = "0px";
    });
}

if (historico) {
    historico.addEventListener('click', function () {
        event.preventDefault();

        if (screen.width > 768) {
            visaoGeral.style.borderLeft = "none";
            editarPerfil.style.borderLeft = "none";
            editarSenha.style.borderLeft = "none";
            historico.style.borderLeft = "5px solid #083990";
            sugestao.style.borderLeft = "none";
        }
        else {
            visaoGeral.style.borderTop = "none";
            editarPerfil.style.borderTop = "none";
            editarSenha.style.borderTop = "none";
            historico.style.borderTop = "5px solid #083990";
            sugestao.style.borderTop = "none";
        }

        divVisaoGeral.style.opacity = "0";
        divVisaoGeral.style.height = "0px";

        divEditarPerfil.style.opacity = "0";
        divEditarPerfil.style.height = "0px";

        divEditarSenha.style.opacity = "0";
        divEditarSenha.style.height = "0px";

        divHistorico.style.opacity = "1";
        divHistorico.style.height = "auto";

        divSugestao.style.opacity = "0";
        divSugestao.style.height = "0px";
    });
}

if (sugestao) {
    sugestao.addEventListener('click', function () {
        event.preventDefault();

        if (screen.width > 768) {
            visaoGeral.style.borderLeft = "none";
            editarPerfil.style.borderLeft = "none";
            editarSenha.style.borderLeft = "none";
            historico.style.borderLeft = "none";
            sugestao.style.borderLeft = "5px solid #083990";
        }
        else {
            visaoGeral.style.borderTop = "none";
            editarPerfil.style.borderTop = "none";
            editarSenha.style.borderTop = "none";
            historico.style.borderTop = "none";
            sugestao.style.borderTop = "5px solid #083990";
        }

        divVisaoGeral.style.opacity = "0";
        divVisaoGeral.style.height = "0px";

        divEditarPerfil.style.opacity = "0";
        divEditarPerfil.style.height = "0px";

        divEditarSenha.style.opacity = "0";
        divEditarSenha.style.height = "0px";

        divHistorico.style.opacity = "0";
        divHistorico.style.height = "0px";

        divSugestao.style.opacity = "1";
        divSugestao.style.height = "auto";
    });
}

var mensagemSucessoSugestaoDiv = document.getElementById("mensagem-sugestao-sucesso-div");

var mensagemErroSugestaoDiv = document.getElementById("mensagem-sugestao-erro-div");

var loadSug = document.getElementById("load-sugestao");

$('#Enviar-Sugestao').click(function () {
    loadSug.style.display = "block";
    var url = "/Home/EnviarSugestao";
    var mensagem = $("#Mensagem").val();
    $.post(url, { mensagem: mensagem })
        .done(function (data) {
            if (data == "Enviado! Obrigado pela sugestão ou por nos reportar um erro. Entraremos em contato em breve!") {
                mensagemSucessoSugestaoDiv.style.opacity = "1";
                mensagemSucessoSugestaoDiv.style.height = "auto !important";
                $("#mensagem-sugestao-sucesso").text(data);
            }
            else {
                mensagemErroSugestaoDiv.style.opacity = "1";
                mensagemErroSugestaoDiv.style.height = "auto !important";
                $("#mensagem-sugestao-erro").text(data);
            }
        });
    loadSug.style.display = "none";
});


var box_livro = document.getElementsByClassName("box-livro");
var modal = document.getElementById("modal");
var darkDiv = document.getElementById("dark-div");
var closeBtn = document.getElementById("close_btn");

if (box_livro.length > 0) {
    for (var i = 0; i < box_livro.length; i++) {
        box_livro[i].addEventListener('click', function (event) {
            var scrollTop = $(document).scrollTop() + 80;

            modal.style.top = scrollTop.toString() + "px";
            modal.classList.add("modalShow");
            darkDiv.classList.add("darkDivShow");
            darkDiv.style.top = scrollTop - 80 + "px";
            document.body.style.overflow = "hidden";
            document.getElementsByTagName('html')[0].style.overflow = "hidden";
            document.documentElement.style.overflow = "hidden";
            console.log(event);
            var value = event.target.getAttribute("value");
            console.log(value);
            preencherModal(value);

        }, false);
    }
}

var CapaLivro = document.getElementById("CapaLivro");
var TituloLivro = document.getElementById("TituloLivro");
var SinopseLivro = document.getElementById("SinopseLivro");
var AutorLivro = document.getElementById("AutorLivro");
var EditoraLivro = document.getElementById("EditoraLivro");
var DataLancLivro = document.getElementById("DataLancLivro");
var IsbnLivro = document.getElementById("IsbnLivro");
var GeneroLivro = document.getElementById("GeneroLivro");
var IdLivro = document.getElementById("IdLivro");
var BotaoLer = document.getElementById("botao-ler");
var BotaoAdicionar = document.getElementById("botao-adicionar");
var BotaoRemover = document.getElementById("botao-remover");
var LoadModal = document.getElementById("load-modal");
var ErroModal = document.getElementById("erro-modal");

function preencherModal(titulo) {
    LoadModal.style.display = "block";
    var url = "/Home/GetLivroByTitle";
    
    $.post(url, { Titulo: titulo })
        .done(function (data) {

            if (data != "Ocorreu algum erro!") {
                data = $.parseJSON(data);
                CapaLivro.src = data.Capa;
                IsOnMyList(data.Titulo);     
                console.log(data);
                TituloLivro.innerHTML = data.Titulo;
                SinopseLivro.innerHTML = data.Sinopse;
                DataLancLivro.innerHTML = "Ano de lançamento: "+ data.Ano_Lanc;
                IsbnLivro.innerHTML = "ISBN: "+ data.Isbn;
                GeneroLivro.innerHTML = "Gênero: " + data.Genero;
                AutorLivro.innerHTML = "Autor: " + data.Autor.Nome;
                EditoraLivro.innerHTML = "Editora: " + data.Editora.Nome;
                IdLivro.value = data.IdLivro;
                LoadModal.style.display = "none";
            }
            else {
                LoadModal.style.display = "none";
                ErroModal.style.display = "block";
                ErroModal.innerHTML = data;
            }
        });



}

function IsOnMyList(Titulo) {
    var url = "/Home/IsOnMyList";
    $.post(url, { NomeLivro: Titulo })
        .done(function (data) {

            if (data) {
                BotaoRemover.style.display = "inline-block";
                BotaoLer.style.display = "inline-block";       
            }
            else {
                BotaoAdicionar.style.display = "inline-block";
                BotaoLer.style.display = "inline-block";       
            }
        });
    return true;
}

if (darkDiv) {
    darkDiv.addEventListener('click', closeModal, false);
    closeBtn.addEventListener('click', closeModal, false);
}


function closeModal() {
    modal.classList.remove("modalShow");
    darkDiv.classList.remove("darkDivShow");
    BotaoLer.style.display = "none";
    BotaoAdicionar.style.display = "none";
    BotaoRemover.style.display = "none";
    CapaLivro.src = "";
    IdLivro.value = "";
    AutorLivro.innerHTML = "";
    EditoraLivro.innerHTML = "";
    TituloLivro.innerHTML = "";
    SinopseLivro.innerHTML = "";
    DataLancLivro.innerHTML = "";
    IsbnLivro.innerHTML = "";
    GeneroLivro.innerHTML = "";
    ErroBtn.innerHTML = "";
    document.documentElement.style.overflowY = "scroll";
    document.getElementsByTagName('html')[0].style.overflow = "scroll";
    ErroModal.style.display = "none";
}

var LoadBtn = document.getElementById("load-btn");
var ErroBtn = document.getElementById("erro-btn");

if (BotaoLer) {
    BotaoLer.addEventListener('click', function () {
        event.preventDefault();

        document.location.href = "/Home/Reader/" + TituloLivro.innerHTML;

    });
}

$('#botao-adicionar').click(function () {
    var url = "/Home/AdicionarFavorito";
    var idLivro = IdLivro.value;
    $.post(url, { IdLivro: idLivro })
        .done(function (data) {
            if (data == "Adicionado") {
                console.log("Adicionado!");
                BotaoAdicionar.style.display = "none";
                BotaoRemover.style.display = "inline-block";
                AtualizarFavoritos();
            }
            else {
                console.log("Erro: " + data);
                ErroBtn.innerHTML = data;
            }
        });
});

$('#botao-remover').click(function () {
    var url = "/Home/RemoverFavorito";
    var idLivro = IdLivro.value;
    $.post(url, { IdLivro: idLivro })
        .done(function (data) {
            if (data == "O livro de id " + TituloLivro.innerHTML + " foi deletado com sucesso da sua lista") {
                console.log("Removido!");
                BotaoAdicionar.style.display = "inline-block";
                BotaoRemover.style.display = "none";
                AtualizarFavoritos();
            }
            else {
                console.log("Erro: " + data);
                ErroBtn.innerHTML = data;
            }
        });
});

var favoritos = document.getElementById("favoritos");
var listaVazia = document.getElementById("ListaVazia");
function AtualizarFavoritos() {
    var url = "/Home/GetFavoritos";
    $.get(url)
        .done(function (data) {

            if (data == "Vazio") {
                while (favoritos.firstChild) {
                    favoritos.removeChild(favoritos.firstChild);
                }
                var vazio = document.createElement("h3");
                vazio.classList.add("text-center");
                vazio.classList.add("padding-top");
                vazio.classList.add("text-white");
                vazio.classList.add("margin-0");
                vazio.innerHTML = "Não há livros na sua lista!";
                favoritos.appendChild(vazio);
            } else {
                if (isJson(data)) {

                    data = $.parseJSON(data);
                    while (favoritos.firstChild) {
                        favoritos.removeChild(favoritos.firstChild);
                    }
                    data.forEach(preencherFavoritos);
                    RefreshBoxLivros();
                }
                else {
                    console.log("Erro: " + data);
                }
            }
         
        });
}

function preencherFavoritos(item) {
    var livro = document.createElement("div");
    livro.classList.add("box-livro");
    if (favoritos.classList.contains("container-flex")) {
        livro.classList.add("box-livro-responsive");
    }
    livro.setAttribute("value", item.Titulo);
    livro.style.backgroundImage = "url(" + item.Capa + ")";
    favoritos.appendChild(livro);
}

function RefreshBoxLivros() {
    var box_livro = document.getElementsByClassName("box-livro");
    for (var i = 0; i < box_livro.length; i++) {
        box_livro[i].addEventListener('click', function (event) {
            var scrollTop = $(document).scrollTop() + 50;

            modal.style.top = scrollTop.toString() + "px";
            modal.classList.add("modalShow");
            darkDiv.classList.add("darkDivShow");
            darkDiv.style.top = scrollTop - 50 + "px";
            document.body.style.overflowY = "hidden";
            console.log(event);
            var value = event.target.getAttribute("value");
            console.log(value);
            preencherModal(value);

        }, false);
    }
}

function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }

    return true;
}

var mensagem = document.getElementsByClassName("mensagem");
var divMensagem = document.getElementsByClassName("alert-div");
var mensagemEmail = document.getElementById("div-mensagem-email");
var mensagemCod = document.getElementById("div-mensagem-cod");
var mensagemRedefinir = document.getElementById("div-mensagem-redefinir");

if (mensagem) {
    for (i = 0; i < mensagem.length; i++) {
        if (mensagem[i].innerHTML == "") {
            divMensagem[i].style.opacity = "0";
            divMensagem[i].style.height = "0px !important";
        }
        else {
            divMensagem[i].style.opacity = "1";
            divMensagem[i].style.height = "auto !important";
        }
    }
}

var erroSenha = document.getElementById("erro-senha");
var erroConfSenha = document.getElementById("erro-conf-senha");

if (erroSenha) {
    if (erroSenha.innerHTML !== "" || erroConfSenha.innerHTML !== "") {
        $("#div-email").hide("slow");
        $("#div-cod").hide("slow");
        $("#div-redefinir").show("slow");
    }
}

var loadEmail = document.getElementById("load-email");
var loadCod = document.getElementById("load-cod");

$('#btn_mandar_cod').click(function () {
    loadEmail.style.display = "block";
    var url = "/Home/RedefinirSenha";
    var email = $("#Email").val();
    $.post(url, { Email: email })
        .done(function (data) {
            if (data == "Um código de redefinição foi enviado para o seu e-mail!") {
                $("#div-email").hide("slow");
                $("#div-cod").show("slow");
            }
            else {
                mensagemEmail.style.opacity = "1";
                mensagemEmail.style.height = "auto !important";
                $("#mensagem-email").text(data);
            }
            loadEmail.style.display = "none";
        });
});

$('#Codigo').blur(function () {
    loadCod.style.display = "block";
    var url = "/Home/VerificarCodVerificacao";
    var codEnviado = $("#Codigo").val();
    $.post(url, { CodEnviado: codEnviado })
        .done(function (data) {
            if (data == "Código Correto") {
                $("#div-cod").hide("slow");
                $("#div-redefinir").show("slow");
                $("#EmailSenha").val($("#Email").val());
            }
            else {
                mensagemCod.style.opacity = "1";
                mensagemCod.style.height = "auto !important";
                $("#mensagem-cod").text(data);
            }
        });
    loadCod.style.display = "none";
});

function GoToPasswordDiv() {
    $("#div-email").hide("slow");
    $("#div-cod").hide("slow");
    $("#div-redefinir").show("slow");
    mensagemRedefinir.style.opacity = "1";
    mensagemRedefinir.style.height = "auto !important";
}

function HideAllDivs() {
    $("#div-email").hide("slow");
    $("#div-cod").hide("slow");
    $("#div-redefinir").hide("slow");
}

var loadTrocarSenha = document.getElementById("load-trocar-senha");
var divMensagemTrocarSenhaErro = document.getElementById("mensagem-trocar-senha-erro-div");
var mensagemTrocarSenhaErro = document.getElementById("mensagem-trocar-senha-erro");
var divMensagemTrocarSenha = document.getElementById("mensagem-trocar-senha-sucesso-div");
var mensagemTrocarSenha = document.getElementById("mensagem-trocar-senha-sucesso");

$('#SenhaAntiga').blur(function () {
    loadTrocarSenha.style.display = "block";
    var url = "/Home/CheckSenha";
    var Senha = $("#SenhaAntiga").val();
    $.post(url, { senha: Senha })
        .done(function (data) {
            if (data == "Certo") {
                divMensagemTrocarSenhaErro.style.opacity = "0";
                divMensagemTrocarSenhaErro.style.height = "0px";
                $("#SenhaAntiga").hide();
                $("#SenhaNova").fadeIn().css('height', '40px');
                $("#SenhaNova").fadeIn().css('opacity', '1');
                $("#SenhaNova").focus();
                $("#SenhaConf").fadeIn().css('height', '40px');
                $("#SenhaConf").fadeIn().css('opacity', '1');
                $("#btn-alterar-senha").removeClass('hidden');
            }
            else {
                divMensagemTrocarSenhaErro.style.opacity = "1";
                divMensagemTrocarSenhaErro.style.height = "auto !important";
                $("#mensagem-trocar-senha-erro").text("Senha inválida");
            }
        });
    loadTrocarSenha.style.display = "none";
});

$('#btn-alterar-senha').click(function () {
    loadTrocarSenha.style.display = "block";
    var url = "/Home/AlterarSenha";
    var Senha = $("#SenhaNova").val();
    var ConfSenha = $("#SenhaConf").val();

    if (Senha.length > 5 && Senha !== "" && Senha == ConfSenha) {
        $.post(url, { senha: Senha })
            .done(function (data) {
                if (data == "Senha Alterada") {
                    divMensagemTrocarSenhaErro.style.opacity = "0";
                    divMensagemTrocarSenhaErro.style.height = "0px";
                    $("#SenhaNova").fadeIn().css('height', '0px');
                    $("#SenhaNova").fadeIn().css('opacity', '0');
                    $("#SenhaConf").fadeIn().css('height', '0px');
                    $("#SenhaConf").fadeIn().css('opacity', '0');
                    $("#btn-alterar-senha").addClass('hidden');
                    divMensagemTrocarSenha.style.opacity = "1";
                    divMensagemTrocarSenha.style.height = "auto";
                    $("#mensagem-trocar-senha-sucesso").text(data + ". Faça o login novamente por favor!");
                    setTimeout(Logout, 2500);
                }
                else {
                    divMensagemTrocarSenhaErro.style.opacity = "1";
                    divMensagemTrocarSenhaErro.style.height = "auto !important";
                    $("#mensagem-trocar-senha-erro").text(data);
                }
            });
    }
    else {
        divMensagemTrocarSenhaErro.style.opacity = "1";
        divMensagemTrocarSenhaErro.style.height = "auto";
        $("#mensagem-trocar-senha-erro").text("Senhas devem ser iguais, não vazias e com mais de 5 caracteres!");
    }

    loadTrocarSenha.style.display = "none";
});

function Logout() {
    document.location.href = '/Home/Logout';
}