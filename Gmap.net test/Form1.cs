using System;
using System.Windows.Forms;
using Gmap.net_test.Class;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.Data.SqlClient;

namespace Gmap.net_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        GMarkerGoogle currentMoveMarker;
        GMapOverlay markersOverlay;
        MarkersClass[] markers;

        string connetionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=markers;Integrated Security=True";
        string commandText;

        public void gMapTest_Load(object sender, EventArgs e)
        {
            gMapTest.Bearing = 0; // Настройки для компонента GMap.
            gMapTest.CanDragMap = true; // Разрешаем перетаскивать карту правой кнопкой мыши
            gMapTest.MarkersEnabled = true; // Показываем маркеры
            gMapTest.MaxZoom = 20; // Максимальное X-кратное приближение
            gMapTest.MinZoom = 12; // Минимальное X-кратное приближение
            gMapTest.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter; // Устанавливаем центр приближения/удаления курсор мыши.
            gMapTest.NegativeMode = false; // Убираем ЧБ режим
            gMapTest.Zoom = 15; // При старте карты исспользуется X-кратное приближение
            // Цепляем Google карты.
            gMapTest.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMapTest.Position = new PointLatLng(54.989646224660376, 82.81009601036776); // Первоночальное положение на карте

            GetDataDB(); // Подключаемся к БД, выкачиваем расположение маркеров

            // Формимурем маркеры (в БД создал 5 штук)
            for (int i = 0; i < markers.Length; i++)
            {
                decimal a = markers[i].PositionLatitude;
                PointLatLng markerPoint = new PointLatLng((double)markers[i].PositionLatitude, (double)markers[i].PositionLongitude);
                GMarkerGoogle marker = new GMarkerGoogle(markerPoint, GMarkerGoogleType.blue);
                marker.ToolTip = new GMapToolTip(marker);
                marker.ToolTipText = markers[i].CarNumber;

                markersOverlay = new GMapOverlay();
                markersOverlay.Markers.Add(marker); // Помещаем маркер на оверлей
                gMapTest.Overlays.Add(markersOverlay); // Помещаем оверлей на оверлей карты
            }
        }

        public void GetDataDB()
        {
            commandText = "SELECT * FROM dbo.All_markers";
            string commandTextCountRow = "SELECT Count(*) FROM dbo.All_markers"; // Колличество маркеров
            int countRow = 0;

            using (SqlConnection connection = new SqlConnection(connetionString))
            {
                // Первым заходом смотрим какой размер массива будет в зависимости от маркера, создаем массив
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(commandTextCountRow, connection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        countRow = (int)sqlDataReader.GetValue(0);
                    }

                    markers = new MarkersClass[countRow];
                    connection.Close();
                }

                // Получаем маркеры
                connection.Open();
                sqlCommand = new SqlCommand(commandText, connection);
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    for (int i = 0; sqlDataReader.Read(); i++)
                    {
                        markers[i] = new MarkersClass();
                        markers[i].ID = (int)sqlDataReader.GetValue(0);
                        markers[i].PositionLatitude = Convert.ToDecimal(sqlDataReader.GetValue(1));
                        markers[i].PositionLongitude = Convert.ToDecimal(sqlDataReader.GetValue(2));
                        markers[i].CarNumber = (string)sqlDataReader.GetValue(3);
                    }
                }
            }
        }

        public void UpdateDataDB()
        {
            using (SqlConnection connection = new SqlConnection(connetionString))
            {
                connection.Open();

                for (int i = 0; i < markers.Length; i++)
                {
                    // В commandText Replace, т.к. при конвертировании из Decimal в String вставляется ","
                    commandText = "UPDATE dbo.All_markers " +
                    "SET latitude = " + Convert.ToString(markers[i].PositionLatitude).Replace(",", ".") + ", Longitude = " + Convert.ToString(markers[i].PositionLongitude).Replace(",", ".") + " " +
                    "WHERE id = " + markers[i].ID;

                    SqlCommand sqlCommand = new SqlCommand(commandText, connection);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        private void gMapTest_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (currentMoveMarker != null)
                {
                    PointLatLng point = gMapTest.FromLocalToLatLng(e.X, e.Y);
                    currentMoveMarker.Position = point;
                }
            }
        }

        private void gMapTest_OnMarkerEnter(GMapMarker item)
        {
            if (item is GMarkerGoogle)
            {
                currentMoveMarker = item as GMarkerGoogle;
            }
        }

        private void gMapTest_OnMarkerLeave(GMapMarker item)
        {
            // Ищем какой маркер переместили
            for (int i = 0; i < markers.Length; i++)
            {
                if (item.ToolTipText == markers[i].CarNumber)
                {
                    markers[i].PositionLatitude = Convert.ToDecimal(item.Position.Lat);
                    markers[i].PositionLongitude = Convert.ToDecimal(item.Position.Lng);

                    break;
                }
            }

            currentMoveMarker = null;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateDataDB();
        }
    }
}
