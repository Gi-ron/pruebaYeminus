using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private static readonly char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        private bool IsFibonacciNumber(int number)
        {
            // Verificar si (5 * n^2 + 4) o (5 * n^2 - 4) es un cuadrado perfecto
            return IsPerfectSquare(5 * number * number + 4) || IsPerfectSquare(5 * number * number - 4);
        }

        private bool IsPerfectSquare(int number)
        {
            int sqrt = (int)Math.Sqrt(number);
            return sqrt * sqrt == number;
        }

        [HttpPost("Encrypt")]
        public IActionResult Encrypt([FromBody] EncryptRequest request)
        {
            if (string.IsNullOrEmpty(request.phrase))
            {
                return BadRequest("Cadena vacía.");
            }

            int key = 5;

            char[] charArray = request.phrase.ToCharArray();

            for (int i = 0; i < charArray.Length; i++)
            {
                char originalChar = charArray[i];

                // Verificar si el carácter es una letra minúscula
                if (char.IsLower(originalChar))
                {
                    int originalIndex = Array.IndexOf(alphabet, originalChar);
                    int newIndex = (originalIndex + key) % 26;
                    char newChar = alphabet[newIndex];
                    charArray[i] = newChar;
                }
                // Verificar si el carácter es una letra mayúscula
                else if (char.IsUpper(originalChar))
                {
                    int originalIndex = Array.IndexOf(alphabet, char.ToLower(originalChar));
                    int newIndex = (originalIndex + key) % 26;
                    char newChar = char.ToUpper(alphabet[newIndex]);
                    charArray[i] = newChar;
                }
            }

            string encryptedData = new string(charArray);
            return Ok(encryptedData);
        }

        [HttpPost("Decrypt")]
        public IActionResult Decrypt([FromBody] DecryptRequest request)
        {
            if (string.IsNullOrEmpty(request.EncryptedData))
            {
                return BadRequest("Cadena vacía");
            }
            //Clave escogida
            int key = 5;
            char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray(); // Array que contiene el alfabeto

            // Convertir la cadena en un arreglo de caracteres
            char[] charArray = request.EncryptedData.ToCharArray();

            for (int i = 0; i < charArray.Length; i++)
            {
                char originalChar = charArray[i];
                // Verificar si el carácter es una letra minúscula
                if (char.IsLower(originalChar))
                {
                    int originalIndex = Array.IndexOf(alphabet, originalChar);
                    int newIndex = (originalIndex - key + 26) % 26;
                    char newChar = alphabet[newIndex];
                    charArray[i] = newChar;
                }
                // Verificar si el carácter es una letra mayúscula
                else if (char.IsUpper(originalChar))
                {
                    int originalIndex = Array.IndexOf(alphabet, char.ToLower(originalChar));
                    int newIndex = (originalIndex - key + 26) % 26;
                    char newChar = char.ToUpper(alphabet[newIndex]);
                    charArray[i] = newChar;
                }
            }

            string decryptedData = new string(charArray);
            return Ok(decryptedData);
        }

        [HttpPost("FibonacciRange")]
        public IActionResult FibonacciRange([FromBody] FibonacciRequest request)
        {
            bool isFibonacci = IsFibonacciNumber(request.Number);
            return Ok(isFibonacci);
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            var products = new List<Producto>
            {
                new Producto
                {
                    Codigo = "123",
                    Descripcion = "juguete inflable",
                    ListaDePrecios = new List<int> { 1000, 2000, 3000 },
                    Imagen = "http://url.delaimagen.com",
                    ProductoParaLaVenta = true,
                    PorcentajeIva = 19
                },
            };

            return Ok(products);
        }

        [HttpPut("products/{codigo}")]
        public IActionResult UpdateProduct(string codigo, [FromBody] Producto updatedProduct)
        {
            // Busca el producto en la lista por su código y actualízalo
            var existingProduct = products.FirstOrDefault(p => p.Codigo == codigo);
            if (existingProduct == null)
            {
                return NotFound("Producto no encontrado");
            }

            // Actualiza las propiedades del producto existente
            existingProduct.Descripcion = updatedProduct.Descripcion;
            existingProduct.ListaDePrecios = updatedProduct.ListaDePrecios;
            existingProduct.Imagen = updatedProduct.Imagen;
            existingProduct.ProductoParaLaVenta = updatedProduct.ProductoParaLaVenta;
            existingProduct.PorcentajeIva = updatedProduct.PorcentajeIva;

            return Ok(existingProduct);
        }

        [HttpDelete("products/{codigo}")]
        public IActionResult DeleteProduct(string codigo)
        {
            var productToRemove = products.FirstOrDefault(p => p.Codigo == codigo);
            if (productToRemove == null)
            {
                return NotFound("Producto no encontrado");
            }

            products.Remove(productToRemove);
            return NoContent();
        }

        [HttpPost("products")]
        public IActionResult CreateProduct([FromBody] Producto newProduct)
        {
            products.Add(newProduct);
            return CreatedAtAction(nameof(GetProducts), new { codigo = newProduct.Codigo }, newProduct);
        }
    }

    public class FibonacciRequest
    {
        public int Number { get; set; }
    }

    public class EncryptRequest
    {
        public string Phrase { get; set; }
    }

    public class DecryptRequest
    {
        public string EncryptedData { get; set; }
    }

    public class Producto
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public List<int> ListaDePrecios { get; set; }
        public string Imagen { get; set; }
        public bool  ProductoParaLaVenta { get; set; }
        public int PorcentajeIva { get; set; }
    }

}
