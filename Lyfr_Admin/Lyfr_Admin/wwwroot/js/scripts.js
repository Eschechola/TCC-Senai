
var btnEnviar = document.getElementById("Enviar");
var inpPesquisa = document.getElementById("Pesquisa");
var formPesquisar = document.getElementById("form-pesquisar");

function ShowLoad() {
    var load = document.getElementsByClassName('black-background-painel')[0];
    load.style.display = "block";
}

function HideLoad() {
    var load = document.getElementsByClassName('black-background-painel')[0];
    load.style.display = "none";
}

if (btnEnviar) {
    btnEnviar.addEventListener('pointerdown', function () {
        event.preventDefault();
        if (inpPesquisa.classList.length > 0) {
            document.getElementById('form-pesquisar').submit();
            ShowLoad();
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

var btn_devare = document.getElementById("btn-devare");

if (btn_devare) {
    btn_devare.addEventListener('click', function () {
        $('#ModalDevare').modal('show');
    });
}
var btn_alter_login = document.getElementById("BtnAlterLogin");
var btn_alter_email = document.getElementById("BtnAlterEmail");
var btn_alter_senha = document.getElementById("BtnAlterSenha");

if (btn_alter_login) {
    btn_alter_login.addEventListener('click', function () {
        $('#ModalAlterLogin').modal('show');
    });
}

if (btn_alter_email) {
    btn_alter_email.addEventListener('click', function () {
        $('#ModalAlterEmail').modal('show');
    });
}

if (btn_alter_senha) {
    btn_alter_senha.addEventListener('click', function () {
        $('#ModalAlterSenha').modal('show');
    });
}

var menuAtivo = false;

function Dropdown() {
    var modal;
    var fundo;

    if (menuAtivo) {
        fundo = document.getElementsByClassName('background-display-order')[0];
        fundo.style.display = "none";

        modal = document.getElementsByClassName('items-dropdown')[0];
        modal.style.visibility = "hidden";
        modal.style.height = "0px";

        setTimeout(function () {
            var menus = document.getElementsByClassName('item-menu-p');
            for (var i = 0; i < menus.length; i++) {
                menus[i].style.opacity = "0";
            }
        }, 0);
        menuAtivo = false;
    }
    else {
        
        fundo = document.getElementsByClassName('background-display-order')[0];
        fundo.style.display = "block";

        modal = document.getElementsByClassName('items-dropdown')[0];
        modal.style.visibility = "visible";
        modal.style.height = "160px";

        setTimeout(function () {
            var menus = document.getElementsByClassName('item-menu-p');
            for (var i = 0; i < menus.length; i++) {
                menus[i].style.opacity = "1";
            }
        }, 500);
        menuAtivo = true;
    }
}

function AlterarHora() {
    var dia = new Date();
    var hora = document.getElementById('hora');

    var horaDisplay = dia.getHours();
    var minutoDisplay = dia.getMinutes();
    var segundoDisplay = dia.getSeconds();

    if (horaDisplay < 10) {
        horaDisplay = '0' + horaDisplay;
    }

    if (minutoDisplay < 10) {
        minutoDisplay = '0' + minutoDisplay;
    }

    if (segundoDisplay < 10) {
        segundoDisplay = '0' + segundoDisplay;
    }

    hora.innerHTML = horaDisplay + " : " + minutoDisplay + " : " + segundoDisplay;
}

function MostrarDataHoraSegundo() {
    var dia = new Date();
    var data = document.getElementById('data');
    var hora = document.getElementById('hora');

    var horaDisplay = dia.getHours();
    var minutoDisplay = dia.getMinutes();
    var segundoDisplay = dia.getSeconds();

    if (horaDisplay < 10) {
        horaDisplay = '0' + horaDisplay;
    }

    if (minutoDisplay < 10) {
        minutoDisplay = '0' + minutoDisplay;
    }

    if (segundoDisplay < 10) {
        segundoDisplay = '0' + segundoDisplay;
    }

    var diaDisplay = dia.getDate();
    var mesDisplay = (dia.getMonth() + 1);

    if (diaDisplay < 10) {
        diaDisplay = '0' + diaDisplay;
    }

    if (mesDisplay < 10) {
        mesDisplay = '0' + mesDisplay;
    }

    hora.innerHTML = horaDisplay + " : " + minutoDisplay + " : " + segundoDisplay;
    data.innerHTML = diaDisplay + " / " + mesDisplay + " / " + dia.getFullYear();


    setInterval(AlterarHora, 1000);
}

function ShowLoadLogin() {
    var load = document.getElementsByClassName('black-background-painel')[0];

    var login = document.getElementById('Login').value;
    var senha = document.getElementById('Senha').value;

    if (login.length > 0 && senha.length > 0) {
        load.style.display = "block";
    }

}

function SelecionarArquivo() {
    document.getElementById('textoLabel').innerHTML = 'Arquivo Selecionado!';
}

var loadFile = function (event) {
    console.log("file");
    var image = document.getElementById('imagemAlterar');
    image.src = URL.createObjectURL(event.target.files[0]);
    image.style.display = "block";
};

function SelecionarCapa() {
    document.getElementById('textoLabelCapa').innerHTML = 'Capa Selecionada!';
}

var loadCapa = function (event) {
    console.log("capa");
    var image = document.getElementById('imagemAlterarCapa');
    image.src = URL.createObjectURL(event.target.files[0]);
    image.style.display = "block";
};


var editoraDeletar = ''
function AtribuirEditora(nome) {
    document.getElementById('nomeEditora').innerHTML = nome;
    editoraDeletar = nome;
}

function DeletarEditoraAJAX() {
    var urlRequest = '/Home/DeletarEditora';

    var objetoEditoraDeletar = {
        nomeEditora: editoraDeletar
    }

    var msgRequest = document.getElementById('msgRequest');
    var contentMsgRequest = document.getElementById('msgRequestContent');
    var esconderDelete = document.getElementsByClassName('modal-body')[0];

    $.ajax({
        url: urlRequest,
        data: objetoEditoraDeletar,
        type: 'DELETE',
        success: function (result) {
            msgRequest.innerHTML = result;
            contentMsgRequest.style.display = 'block';
            esconderDelete.style.display = 'none';
            HideLoad();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            msgRequest.innerHTML = thrownError;
            contentMsgRequest.style.display = 'block';
            esconderDelete.style.display = 'none';
            HideLoad();
        }
    });
}

function RemontarModal() {
    var conteudoModal = `<br>
                         <p>Digite aqui seu e-mail e nós iremos enviar um código de confirmação:</p>
                         <br>

                         <input type="text" id="inputEsqueceuSenha" name="email" required placeholder="Digite seu email..." />
                         <button id="buttonEsqueceuSenha" onclick="RecuperarSenhaAJAX();">Enviar</button>`;

    document.getElementsByClassName('recuperar-codigo')[0].innerHTML = conteudoModal;

    $('#modalSenha').modal('toggle'); 
}

function AlterarSenhaAJAX() {
    ShowLoad();
    var urlRequest = '/Home/AlterarSenha';
    var senhaInput = document.getElementsByName('senha')[0].value;
    var confirmacaoSenhaInput = document.getElementsByName('confirmacaoSenha')[0].value;

    var objetoEditoraDeletar = {
        senha: senhaInput,
        confirmacaoSenha: confirmacaoSenhaInput,
        email: emailRecebido,
    }

    $.ajax({
        url: urlRequest,
        data: objetoEditoraDeletar,
        type: 'PUT',
        success: function (result) {
            var conteudoModal = document.getElementsByClassName('recuperar-codigo')[0].innerHTML = '<div class="result">' + result +'<br> <button onclick="RemontarModal()">Voltar</button></div>';
            HideLoad();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            var conteudoModal = document.getElementsByClassName('recuperar-codigo')[0].innerHTML = '<div class="result">' + thrownError + '<br> <button onclick="RemontarModal()">Voltar</button></div>';
            HideLoad();
        }
    });
}

var codigoEsqueciSenha = ''
var emailRecebido = '';

function VerificarCodigo() {
    var codigo = document.getElementById('inputEsqueceuSenha').value;
    var conteudoModal = document.getElementsByClassName('recuperar-codigo')[0].innerHTML = '<br><input type="text" maxlength="5" id="inputEsqueceuSenha" name="codigo" required placeholder="Digite o código aqui..." />' +
                                                                                           '<button id="buttonEsqueceuSenha" onclick="VerificarCodigo();">Verificar</button>';

    if (codigo == codigoEsqueciSenha) {
        var inputCodigo = `
                                <input type="text" id="inputEsqueceuSenha" name="email" readonly required value="${emailRecebido}"/><br><br>
                                <input type="text" id="inputEsqueceuSenha" name="senha" required placeholder="Digite sua nova senha..."/><br><br>
                                <input type="text" id="inputEsqueceuSenha" name="confirmacaoSenha" required placeholder="Digite novamente sua nova senha..." /><br>
                                <button id="buttonEsqueceuSenha" onclick="AlterarSenhaAJAX()">Alterar</button>
                            `;

        var conteudoModal = document.getElementsByClassName('recuperar-codigo')[0].innerHTML = inputCodigo;
    }
    else {
        var conteudoAlterado = document.getElementsByClassName('recuperar-codigo')[0].innerHTML = "<br><center><h3 class=\"error-code\">O código está inválido!<h3></center>" + conteudoModal;
    }
}

function RecuperarSenhaAJAX() {
    ShowLoad();

    var urlRequest = '/Home/EsqueciSenha';
    emailRecebido = document.getElementById('inputEsqueceuSenha').value;
    codigoEsqueciSenha = Math.random().toString(36).substring(8).toUpperCase();

    var objetoSenha = {
        Email: emailRecebido,
        CodigoGerado: codigoEsqueciSenha,
    }

    $.ajax({
        url: urlRequest,
        data: objetoSenha,
        type: 'POST',
        success: function (result) {
            var inputCodigo = '<br><br><br><input type="text" maxlength="5" id="inputEsqueceuSenha" name="codigo" required placeholder="Digite o código aqui..." />' +
                              '<button id="buttonEsqueceuSenha" onclick="VerificarCodigo();">Verificar</button>';

            var conteudoModal = document.getElementsByClassName('recuperar-codigo')[0].innerHTML = inputCodigo;
            HideLoad();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            var conteudoModal = document.getElementsByClassName('recuperar-codigo')[0].innerHTML = '<div class="result"><br>Administrador não encontrado.<br> <button onclick="RemontarModal()">Voltar</button></div>';
            HideLoad();
        }
    });
}


var generoDeletar = '';
var livroDeletar = '';
function AtribuirGenero(nome) {
    document.getElementById('nomeGenero').innerHTML = nome;
    generoDeletar = nome;
}

function AtribuirLivro(nome) {
    document.getElementById('nomeLivro').innerHTML = nome;
    livroDeletar = nome;
}

function DeletarGeneroAJAX() {
    var urlRequest = '/Home/DeletarGenero';

    var objetoGeneroDeletar = {
        nomeGenero: generoDeletar
    }

    var msgRequest = document.getElementById('msgRequest');
    var contentMsgRequest = document.getElementById('msgRequestContent');
    var esconderDelete = document.getElementsByClassName('modal-body')[0];

    $.ajax({
        url: urlRequest,
        data: objetoGeneroDeletar,
        type: 'DELETE',
        success: function (result) {
            msgRequest.innerHTML = result;
            contentMsgRequest.style.display = 'block';
            esconderDelete.style.display = 'none';
            HideLoad();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            msgRequest.innerHTML = thrownError;
            contentMsgRequest.style.display = 'block';
            esconderDelete.style.display = 'none';
            HideLoad();
        }
    });
}

function DeletarLivroAJAX() {
    var urlRequest = '/Home/DeletarLivro';

    var objetoLivroDeletar = {
        nomeLivro: livroDeletar
    }

    var msgRequest = document.getElementById('msgRequest');
    var contentMsgRequest = document.getElementById('msgRequestContent');
    var esconderDelete = document.getElementsByClassName('modal-body')[0];

    $.ajax({
        url: urlRequest,
        data: objetoLivroDeletar,
        type: 'DELETE',
        success: function (result) {
            msgRequest.innerHTML = result;
            contentMsgRequest.style.display = 'block';
            esconderDelete.style.display = 'none';
            HideLoad();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            msgRequest.innerHTML = thrownError;
            contentMsgRequest.style.display = 'block';
            esconderDelete.style.display = 'none';
            HideLoad();
        }
    });
}