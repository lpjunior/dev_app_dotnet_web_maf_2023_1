namespace BibliotecaApp.Validations
{
    public static class Validacoes
    {
        public static string ValidarTitulo(this string titulo) {
            var tituloTrim = titulo.Trim();

            if (string.IsNullOrWhiteSpace(tituloTrim))
            {
                return "O título é obrigatório.";
            }

            if (tituloTrim.Length < 3 || tituloTrim.Length > 255) {
                return "O título deve ter entre 3 e 255 caracteres.";
            }

            return string.Empty;
        }

        public static string ValidarAutor(this string autor) {
            var autorTrim = autor.Trim();

            if (string.IsNullOrWhiteSpace(autorTrim))
            {
                return "O autor é obrigatório.";
            }

            if (autorTrim.Length < 3 || autorTrim.Length > 100)
            {
                return "O autor deve ter entre 3 e 100 caracteres.";
            }

            return string.Empty;
        }

        public static string ValidarAnoPublicacao(this int anoPublicacao) {
            if (anoPublicacao < 1700 || anoPublicacao > DateTime.Now.Year)
            {
                return "O ano de publicação deve ter entre 1700 e o ano atual.";
            }

            return string.Empty;
        }

        public static string ValidarQuantidadeDisponivel(this int quantidadeDisponivel)
        {
            if (quantidadeDisponivel < 0 || quantidadeDisponivel > 5)
            {
                return "A quantidade disponível deve ser entre 0 e 5 livros.";
            }

            return string.Empty;
        }

        public static string ValidarISBN(this string isbn)
        {
            isbn = isbn.Trim().Replace("-", "");

            if (string.IsNullOrWhiteSpace(isbn))
            {
                return "O ISBN é obrigatório.";
            }

            if (isbn.Length == 10)
            {
                if(!IsValidISBN10(isbn))
                    return "O ISBN-10 não segue o padrão internacional.";
            }
            else if (isbn.Length == 13)
            {
                if (!IsValidISBN13(isbn))
                    return "O ISBN-13 não segue o padrão internacional.";
            }
            else
            {
                return "O ISBN deve seguir o padrão internacional de 10 ou 13 dígitos.";
            }

            return string.Empty;
        }

        private static bool IsValidISBN10(string isbn)
        {
            if (!isbn.All(char.IsDigit))
            {
                return false;
            }

            var soma = 0;
            for(var i = 0; i < 9; i++)
            {
                soma += (i + 1) * int.Parse(isbn[i].ToString());
            }

            var resto = soma % 11;
            var verificaDigito = resto == 10 ? 'X' : char.Parse(resto.ToString());

            return isbn[9] == verificaDigito;
        }

        private static bool IsValidISBN13(string isbn)
        {
            if (!isbn.All(char.IsDigit))
            {
                return false;
            }

            var soma = 0;

            for (var i = 0; i < 12; i++)
            {
                var multiplicador = ((i+1) % 2 == 0) ? 3 : 1;
                soma += multiplicador * int.Parse(isbn[i].ToString());
            }

            var resto = soma % 10;
            var verificaDigito = resto == 0 ? 0 : 10 - resto;

            return isbn[12] == char.Parse(verificaDigito.ToString());
        }
    }
}