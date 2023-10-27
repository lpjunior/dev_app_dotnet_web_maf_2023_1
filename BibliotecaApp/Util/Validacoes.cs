namespace BibliotecaApp.Util
{
    public static class Validacoes
    {
        public static void ValidarTitulo(this string titulo) {
            var tituloTrim = titulo.Trim();

            if (string.IsNullOrWhiteSpace(tituloTrim))
            {
                throw new ArgumentException("O título é obrigatório.");
            }

            if(tituloTrim.Length < 3 || tituloTrim.Length > 255) {
                throw new ArgumentException("O título deve ter entre 3 e 255 caracteres.");
            }
        }

        public static void ValidarAutor(this string autor) {
            var autorTrim = autor.Trim();

            if (string.IsNullOrWhiteSpace(autorTrim))
            {
                throw new ArgumentException("O autor é obrigatório.");
            }

            if (autorTrim.Length < 3 || autorTrim.Length > 100)
            {
                throw new ArgumentException("O autor deve ter entre 3 e 100 caracteres.");
            }
        }

        public static void ValidarAnoPublicacao(this int anoPublicacao) {
            if (anoPublicacao < 1700 || anoPublicacao > DateTime.Now.Year)
            {
                throw new ArgumentException("O ano de publicação deve ter entre 1700 e o ano atual.");
            }
        }

        public static void ValidarQuantidadeDisponivel(this int quantidadeDisponivel)
        {
            if (quantidadeDisponivel < 0 || quantidadeDisponivel > 5)
            {
                throw new ArgumentException("A quantidade disponível deve ser entre 0 e 5 livros.");
            }
        }

        public static void ValidarISBN(this string isbn)
        {
            isbn = isbn.Trim().Replace("-", "");

            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException("O ISBN é obrigatório.");
            }

            if (isbn.Length == 10)
            {
                if(!ValidaISBN10(isbn))
                    throw new ArgumentException("O ISBN-10 não segue o padrão internacional.");
            }
            else if (isbn.Length == 13)
            {
                if (!ValidaISBN13(isbn))
                    throw new ArgumentException("O ISBN-13 não segue o padrão internacional.");
            }
            else
            {
                throw new ArgumentException("O ISBN deve seguir o padrão internacional de 10 ou 13 dígitos.");
            }
        }

        private static bool ValidaISBN10(string isbn)
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

        private static bool ValidaISBN13(string isbn)
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