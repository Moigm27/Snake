namespace Snake
{
    public partial class Form1 : Form
    {
        #region Variables_constantes

        #endregion


        #region Variables 
        /// <summary>
        /// Define las constantes y variables utilizadas en el juego de Snake.
        /// - tamCelda: El tamaño en píxeles de cada celda en el tablero.
        /// - anchoTablero: El número de celdas a lo ancho del tablero.
        /// - alturaTablero: El número de celdas de altura del tablero.
        /// - tamInicialSerpiente: El tamaño inicial de la serpiente al inicio del juego.
        /// - pictureBox: El control PictureBox utilizado para mostrar el juego en la interfaz de usuario.
        /// - serpiente: Una lista que almacena las posiciones de los segmentos de la serpiente en el tablero.
        /// - comidita: La posición de la comida en el tablero.
        /// - direccion: La dirección actual de movimiento de la serpiente.
        /// - hiloJuego: El hilo utilizado para ejecutar el bucle principal del juego.
        /// - jugando: Indica si el juego está en curso.
        /// - Direction: Enumerado que define las direcciones posibles de movimiento de la serpiente (arriba, abajo, izquierda, derecha).
        /// </summary>
        private const int tamCelda = 20;
        private const int anchoTablero = 20;
        private const int alturaTablero = 20;
        private const int tamInicialSerpiente = 3;

        private PictureBox pictureBox;
        private List<Point> serpiente;
        private Point comidita;
        private Direction direccion;
        private Thread hiloJuego;
        private bool jugando;

        private enum Direction {  Down, Left, Right, Up }

        #endregion
        public Form1()
        {
            InitializeComponent();
            Jugar();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Inicia el juego de Snake.
        /// Crea una nueva instancia del juego con las configuraciones iniciales, 
        /// como la dirección, la posición de la serpiente y la generación de comida.
        /// También crea un nuevo PictureBox para visualizar el juego en la interfaz de usuario 
        /// y comienza un nuevo hilo para ejecutar el bucle principal del juego.
        /// </summary>
        private void Jugar()
        {
            jugando = true;
            direccion = Direction.Right;
            serpiente = new List<Point>();
            serpiente.Add(new Point(anchoTablero / 2, alturaTablero / 2));
            GenerarComidita();
            pictureBox = new PictureBox();
            pictureBox.Size = new Size(anchoTablero * tamCelda, alturaTablero * tamCelda);
            pictureBox.Location = new Point(10, 10);
            pictureBox.BackColor = Color.Black;
            pictureBox.Paint += PintarJuego;
            Controls.Add(pictureBox);

            hiloJuego = new Thread(BucleJuego);
            hiloJuego.Start();
        }

        /// <summary>
        /// Manejador de eventos para dibujar la serpiente y la comida en un control gráfico.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Los argumentos del evento Paint que contienen información sobre cómo dibujar.</param>
        private void PintarJuego(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DibujarSerpiente(g);
            DibujarComidita(g);
        }

        /// <summary>
        /// Dibuja la serpiente en el tablero de juego utilizando el objeto Graphics especificado.
        /// Itera a través de cada segmento de la serpiente y dibuja un rectángulo verde en la posición correspondiente en el tablero de juego.
        /// Cada segmento de la serpiente se representa como un rectángulo de tamaño igual al tamaño de una celda en el tablero.
        /// </summary>
        /// <param name="g">El objeto Graphics utilizado para dibujar en el tablero de juego.</param>

        private void DibujarSerpiente(Graphics g)
        {
            foreach (Point segment in serpiente)
            {
                g.FillRectangle(Brushes.Green, segment.X * tamCelda, segment.Y * tamCelda, tamCelda, tamCelda);
            }
        }

        /// <summary>
        /// Dibuja la comida en el tablero de juego utilizando el objeto Graphics especificado.
        /// Dibuja un rectángulo rojo en la posición de la comida en el tablero de juego.
        /// La posición de la comida se especifica por la coordenada (X, Y) de la variable comidita, multiplicada por el tamaño de una celda en el tablero.
        /// </summary>
        /// <param name="g">El objeto Graphics utilizado para dibujar en el tablero de juego.</param>

        private void DibujarComidita(Graphics g)
        {
            g.FillRectangle(Brushes.Red, comidita.X * tamCelda, comidita.Y * tamCelda, tamCelda, tamCelda);
        }

        /// <summary>
        /// Genera la posición aleatoria de la comida en el tablero de juego.
        /// Utiliza un objeto Random para generar coordenadas aleatorias dentro del rango del tablero de juego.
        /// La posición de la comida se establece en una nueva coordenada (X, Y) generada aleatoriamente.
        /// </summary>
        private void GenerarComidita()
        {
            Random random = new Random();
            comidita = new Point(random.Next(anchoTablero), random.Next(alturaTablero));
        }

        /// <summary>
        /// Controla el bucle principal del juego de Snake.
        /// Mueve la serpiente, verifica si ha ocurrido alguna colisión y actualiza la visualización del juego en el PictureBox.
        /// El bucle se ejecuta mientras el juego esté en curso (jugando = true).
        /// </summary>
        private void BucleJuego()
        {
            while (jugando)
            {
                MoverSerpiente();
                revisarChoque();
                pictureBox.Invalidate();
                Thread.Sleep(100); // Velocidad del juego
            }
        }

        /// <summary>
        /// Mueve la serpiente en el tablero de juego según la dirección actual.
        /// Calcula la nueva posición de la cabeza de la serpiente basada en la dirección actual.
        /// Inserta la nueva posición de la cabeza en la lista de segmentos de la serpiente y elimina el último segmento si la serpiente no ha comido la comida.
        /// Genera una nueva posición para la comida si la cabeza de la serpiente alcanza la comida.
        /// </summary>

        private void MoverSerpiente()
        {
            Point cabeza = serpiente[0];
            Point nuevaCabeza = new Point(cabeza.X, cabeza.Y);

            switch (direccion)
            {
                case Direction.Up:
                    nuevaCabeza.Y--;
                    break;
                case Direction.Down:
                    nuevaCabeza.Y++;
                    break;
                case Direction.Left:
                    nuevaCabeza.X--;
                    break;
                case Direction.Right:
                    nuevaCabeza.X++;
                    break;
            }

            serpiente.Insert(0, nuevaCabeza);
            if (nuevaCabeza == comidita)
            {
                serpiente.RemoveAt(serpiente.Count - 1);
            }
            else
            {
                GenerarComidita();
            }
        }

        /// <summary>
        /// Verifica si ha ocurrido una colisión de la cabeza de la serpiente con las paredes del tablero o con el cuerpo de la serpiente.
        /// Si la cabeza de la serpiente está fuera del tablero o choca con su propio cuerpo, finaliza el juego y muestra un mensaje de pérdida.
        /// </summary>

        private void revisarChoque()
        {
            Point cabeza = serpiente[0];

            if (cabeza.X < 0 || cabeza.X >= anchoTablero || cabeza.Y < 0 || cabeza.Y >= alturaTablero)
            {
                jugando = false;
                MessageBox.Show("Perdiste!!!!");
                return;
            }

            for (int i = 1; i < serpiente.Count; i++)
            {
                if (cabeza == serpiente[i])
                {
                    jugando = false;
                    MessageBox.Show("Perdiste!!!!");
                    return;
                }
            }
        }
        /// <summary>
        /// Procesa las teclas presionadas por el usuario y actualiza la dirección de movimiento de la serpiente en función de la tecla presionada.
        /// Si se presiona una tecla de dirección válida y no se está moviendo en la dirección opuesta, actualiza la dirección de la serpiente.
        /// </summary>
        /// <param name="msg">El mensaje que se va a procesar.</param>
        /// <param name="keyData">Las teclas que se han presionado.</param>
        /// <returns>true si el carácter se procesa en el método; de lo contrario, false.</returns>

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    if (direccion != Direction.Down)
                        direccion = Direction.Up;
                    break;
                case Keys.Down:
                    if (direccion != Direction.Up)
                        direccion = Direction.Down;
                    break;
                case Keys.Left:
                    if (direccion != Direction.Right)
                        direccion = Direction.Left;
                    break;
                case Keys.Right:
                    if (direccion != Direction.Left)
                        direccion = Direction.Right;
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
