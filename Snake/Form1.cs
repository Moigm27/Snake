namespace Snake
{
    public partial class Form1 : Form
    {
        #region Variables_constantes

        #endregion

        #region Variables 
        /// <summary>
        /// Define las constantes y variables utilizadas en el juego de Snake.
        /// - tamCelda: El tama�o en p�xeles de cada celda en el tablero.
        /// - anchoTablero: El n�mero de celdas a lo ancho del tablero.
        /// - alturaTablero: El n�mero de celdas de altura del tablero.
        /// - tamInicialSerpiente: El tama�o inicial de la serpiente al inicio del juego.
        /// - pictureBox: El control PictureBox utilizado para mostrar el juego en la interfaz de usuario.
        /// - serpiente: Una lista que almacena las posiciones de los segmentos de la serpiente en el tablero.
        /// - comidita: La posici�n de la comida en el tablero.
        /// - direccion: La direcci�n actual de movimiento de la serpiente.
        /// - hiloJuego: El hilo utilizado para ejecutar el bucle principal del juego.
        /// - jugando: Indica si el juego est� en curso.
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
        private Thread hiloDiff;
        private bool jugando;

        private int dificultad;
        private string ColorPrin;
        private List<PowerUp> powerUps;
        private Random random = new Random();

        private enum Direction { Down, Left, Right, Up }

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Inicia el juego de Snake.
        /// Crea una nueva instancia del juego con las configuraciones iniciales, 
        /// como la direcci�n, la posici�n de la serpiente y la generaci�n de comida.
        /// Tambi�n crea un nuevo PictureBox para visualizar el juego en la interfaz de usuario 
        /// y comienza un nuevo hilo para ejecutar el bucle principal del juego.
        /// </summary>
        private void Jugar()
        {


            jugando = true;
            direccion = Direction.Right;
            serpiente = new List<Point>();
            serpiente.Add(new Point(anchoTablero / 2, alturaTablero / 2));
            GenerarComidita();

            powerUps = new List<PowerUp>();
            GenerarPowerUp();

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
        /// Manejador de eventos para dibujar la serpiente y la comida en un control gr�fico.
        /// </summary>
        /// <param name="sender">El objeto que desencaden� el evento.</param>
        /// <param name="e">Los argumentos del evento Paint que contienen informaci�n sobre c�mo dibujar.</param>
        private void PintarJuego(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DibujarSerpiente(g);
            DibujarComidita(g);
            DibujarPowerUps(g);
        }

        /// <summary>
        /// Dibuja la serpiente en el tablero de juego utilizando el objeto Graphics especificado.
        /// Itera a trav�s de cada segmento de la serpiente y dibuja un rect�ngulo verde en la posici�n correspondiente en el tablero de juego.
        /// Cada segmento de la serpiente se representa como un rect�ngulo de tama�o igual al tama�o de una celda en el tablero.
        /// </summary>
        /// <param name="g">El objeto Graphics utilizado para dibujar en el tablero de juego.</param>
        private void DibujarSerpiente(Graphics g)
        {
            for (int i = 0; i < serpiente.Count; i++)
            {
                Point segment = serpiente[i];

                // Alterna entre el color de la cabeza y el cuerpo
                Brush colorSegmento = (i % 2 == 0) ? new SolidBrush(colorCabeza) : new SolidBrush(colorCuerpo);

                g.FillRectangle(colorSegmento, segment.X * tamCelda, segment.Y * tamCelda, tamCelda, tamCelda);
            }
        }

        /// <summary>
        /// Dibuja la comida en el tablero de juego utilizando el objeto Graphics especificado.
        /// Dibuja un rect�ngulo rojo en la posici�n de la comida en el tablero de juego.
        /// La posici�n de la comida se especifica por la coordenada (X, Y) de la variable comidita, multiplicada por el tama�o de una celda en el tablero.
        /// </summary>
        /// <param name="g">El objeto Graphics utilizado para dibujar en el tablero de juego.</param>
        private void DibujarComidita(Graphics g)
        {
            g.FillRectangle(Brushes.Red, comidita.X * tamCelda, comidita.Y * tamCelda, tamCelda, tamCelda);
        }

        /// <summary>
        /// Genera la posici�n aleatoria de la comida en el tablero de juego.
        /// Utiliza un objeto Random para generar coordenadas aleatorias dentro del rango del tablero de juego.
        /// La posici�n de la comida se establece en una nueva coordenada (X, Y) generada aleatoriamente.
        /// </summary>
        private void GenerarComidita()
        {
            Random random = new Random();
            comidita = new Point(random.Next(anchoTablero), random.Next(alturaTablero));
        }

        /// <summary>
        /// Controla el bucle principal del juego de Snake.
        /// Mueve la serpiente, verifica si ha ocurrido alguna colisi�n y actualiza la visualizaci�n del juego en el PictureBox.
        /// El bucle se ejecuta mientras el juego est� en curso (jugando = true).
        /// </summary>
        private void BucleJuego()
        {
            int contadorPowerUp = 0;

            while (jugando)
            {
                MoverSerpiente();
                revisarChoque();
                pictureBox.Invalidate();

                // Generar un power-up cada cierto tiempo
                contadorPowerUp++;
                if (contadorPowerUp >= 50) // Cada 50 ciclos de juego
                {
                    GenerarPowerUp();
                    contadorPowerUp = 0;
                }

                Thread.Sleep(dificultad); // Velocidad del juego
            }
        }

        /// <summary>
        /// Mueve la serpiente en el tablero de juego seg�n la direcci�n actual.
        /// Calcula la nueva posici�n de la cabeza de la serpiente basada en la direcci�n actual.
        /// Inserta la nueva posici�n de la cabeza en la lista de segmentos de la serpiente y elimina el �ltimo segmento si la serpiente no ha comido la comida.
        /// Genera una nueva posici�n para la comida si la cabeza de la serpiente alcanza la comida.
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

            // Comprueba si la serpiente come la comida
            if (nuevaCabeza == comidita)
            {
                GenerarComidita();
            }
            else
            {
                serpiente.RemoveAt(serpiente.Count - 1);
            }

            // Comprueba si la serpiente recoge un power-up
            for (int i = 0; i < powerUps.Count; i++)
            {
                if (nuevaCabeza == powerUps[i].Position)
                {
                    AplicarPowerUp(powerUps[i]);
                    powerUps.RemoveAt(i); // Elimina el power-up del tablero
                    break;
                }
            }
        }

        private Color colorCabeza = Color.Red;  // Color por defecto de la cabeza
        private Color colorCuerpo = Color.Blue;  // Color por defecto del cuerpo

        /// <summary>
        /// Verifica si ha ocurrido una colisi�n de la cabeza de la serpiente con las paredes del tablero o con el cuerpo de la serpiente.
        /// Si la cabeza de la serpiente est� fuera del tablero o choca con su propio cuerpo, finaliza el juego y muestra un mensaje de p�rdida.
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
        /// Procesa las teclas presionadas por el usuario y actualiza la direcci�n de movimiento de la serpiente en funci�n de la tecla presionada.
        /// Si se presiona una tecla de direcci�n v�lida y no se est� moviendo en la direcci�n opuesta, actualiza la direcci�n de la serpiente.
        /// </summary>
        /// <param name="msg">El mensaje que se va a procesar.</param>
        /// <param name="keyData">Las teclas que se han presionado.</param>
        /// <returns>true si el car�cter se procesa en el m�todo; de lo contrario, false.</returns>
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

        private void button1_Click(object sender, EventArgs e)
        {
            DefinirDificultad();
            Thread.Sleep(1000);
            panel1.Visible = false;
            Jugar();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void DefinirDificultad()
        {
            int nivel = nvlDiff.SelectedIndex;
            switch (nivel)
            {
                case 0:
                    dificultad = 140;
                    break;

                case 1:
                    dificultad = 110;
                    break;

                case 2:
                    dificultad = 80;
                    break;
            }
        }
        private void ReiniciarJuego()
        {
            if (hiloJuego != null && hiloJuego.IsAlive)
            {
                jugando = false;
                hiloJuego.Join();
            }
            Jugar();
        }

        private void ColorPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ColorPrincipal.SelectedItem.ToString())
            {
                case "Rojo":
                    colorCabeza = Color.Red;
                    break;
                case "Azul":
                    colorCabeza = Color.Blue;
                    break;
                case "Verde":
                    colorCabeza = Color.Green;
                    break;
                case "Amarillo":
                    colorCabeza = Color.Yellow;
                    break;
                case "Naranja":
                    colorCabeza = Color.Orange;
                    break;
                case "Morado":
                    colorCabeza = Color.Purple;
                    break;
            }
        }


        private void GenerarPowerUp()
        {
            PowerUp.PowerUpType tipo = (PowerUp.PowerUpType)random.Next(0, 4); // Elige un tipo aleatorio
            Point posicion = new Point(random.Next(anchoTablero), random.Next(alturaTablero)); // Posici�n aleatoria
            powerUps.Add(new PowerUp(posicion, tipo));
        }
        private void DibujarPowerUps(Graphics g)
        {
            foreach (PowerUp powerUp in powerUps)
            {
                Brush color = Brushes.Yellow; // Color base para los power-ups
                switch (powerUp.Tipo)
                {
                    case PowerUp.PowerUpType.SpeedUp:
                        color = Brushes.Green;
                        break;
                    case PowerUp.PowerUpType.SlowDown:
                        color = Brushes.Blue;
                        break;
                    case PowerUp.PowerUpType.Grow:
                        color = Brushes.Purple;
                        break;

                }
                g.FillRectangle(color, powerUp.Position.X * tamCelda, powerUp.Position.Y * tamCelda, tamCelda, tamCelda);
            }
        }
        private void AplicarPowerUp(PowerUp powerUp)
        {
            switch (powerUp.Tipo)
            {
                case PowerUp.PowerUpType.SpeedUp:
                    dificultad = Math.Max(10, dificultad - 30); // Aumenta la velocidad
                    break;
                case PowerUp.PowerUpType.SlowDown:
                    dificultad += 70; // Disminuye la velocidad
                    break;
                case PowerUp.PowerUpType.Grow:
                    serpiente.Add(serpiente[serpiente.Count - 1]); // Aumenta el tama�o de la serpiente
                    break;

            }
        }

        private void ColorSecundario_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ColorSecundario.SelectedItem.ToString())
            {
                case "Rojo":
                    colorCuerpo = Color.Red;
                    break;
                case "Azul":
                    colorCuerpo = Color.Blue;
                    break;
                case "Verde":
                    colorCuerpo = Color.Green;
                    break;
                case "Amarillo":
                    colorCuerpo = Color.Yellow;
                    break;
                case "Naranja":
                    colorCuerpo = Color.Orange;
                    break;
                case "Morado":
                    colorCuerpo = Color.Purple;
                    break;
            }
        }


        private void btnMultijugador_Click(object sender, EventArgs e)
        {

        }



        //private void 


    }

    class PowerUp
    {


        public Point Position { get; set; }
        public PowerUpType Tipo { get; set; }

        public enum PowerUpType { SpeedUp, SlowDown, Grow, Shrink }

        public PowerUp(Point position, PowerUpType tipo)
        {
            Position = position;
            Tipo = tipo;
        }

    }
}
