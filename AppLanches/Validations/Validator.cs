using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppLanches.Validations
{
    public class Validator : IValidator
    {
        public string NameError { get ; set; }
        public string EmailError { get; set; }
        public string TelephoneError { get; set; }
        public string PasswordError { get; set; }

        private const string NomeVazioErroMsg = "Por favor, informe o seu nome.";
        private const string NomeInvalidoErroMsg = "Por favor, informe um nome válido.";
        private const string EmailVazioErroMsg = "Por favor, informe um email.";
        private const string EmailInvalidoErroMsg = "Por favor, informe um email válido.";
        private const string TelefoneVazioErroMsg = "Por favor, informe um telefone.";
        private const string TelefoneInvalidoErroMsg = "Por favor, informe um telefone válido.";
        private const string SenhaVazioErroMsg = "Por favor, informe a senha.";
        private const string SenhaInvalidaErroMsg = "A senha deve conter pelo menos 8 caracteres, incluindo letras e números.";

        public Task<bool> Validar(string nome, string email, string telefone, string senha)
        {
            var isNomeValido = ValidarNome(nome);
            var isEmailValido = ValidarEmail(email);
            var isTelefoneValido = ValidarTelefone(telefone);
            var isSenhaValida = ValidarSenha(senha);

            return Task.FromResult(isNomeValido && isEmailValido && isTelefoneValido && isSenhaValida);
        }

        private bool ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                NameError = NomeVazioErroMsg;
                return false;
            }

            if (nome.Length < 3)
            {
                NameError = NomeInvalidoErroMsg;
                return false;
            }

            NameError = "";
            return true;
        }

        private bool ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                EmailError = EmailVazioErroMsg;
                return false;
            }

            if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                EmailError = EmailInvalidoErroMsg;
                return false;
            }

            EmailError = "";
            return true;
        }

        private bool ValidarTelefone(string telefone)
        {
            if (string.IsNullOrEmpty(telefone))
            {
                TelephoneError = TelefoneVazioErroMsg;
                return false;
            }

            if (telefone.Length < 12)
            {
                TelephoneError = TelefoneInvalidoErroMsg;
                return false;
            }

            TelephoneError = "";
            return true;
        }

        private bool ValidarSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha))
            {
                PasswordError = SenhaVazioErroMsg;
                return false;
            }

            if (senha.Length < 8 || !Regex.IsMatch(senha, @"[a-zA-Z]") || !Regex.IsMatch(senha, @"\d"))
            {
                PasswordError = SenhaInvalidaErroMsg;
                return false;
            }

            PasswordError = "";
            return true;
        }

    }
}
