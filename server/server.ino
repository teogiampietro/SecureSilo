#include <ESP8266WiFi.h>
#include <ESP8266WebServer.h>

//parametros para el access point
char * ssid_ap = "ESP8266_SSID";
char * password_ap = "12345678";
IPAddress ip(192, 168, 2, 1);
IPAddress gateway(192, 168, 2, 1);
IPAddress subnet(255, 255, 255, 0);

//server object
ESP8266WebServer server(80);
String cadena = "sin datos";
float sensor_value1 = 0.0;
float sensor_value2 = 0.0;
int sensor_value3 = 0;

void setup() {
  WiFi.mode(WIFI_AP);
  WiFi.softAPConfig(ip, gateway, subnet);
  WiFi.softAP(ssid_ap, password_ap);
  //print ip address as a sanity check
  Serial.begin(115200);
  Serial.println();
  Serial.print("IP Address: "); Serial.println(WiFi.softAPIP());
  server.on("/", handleIndex);
  server.on("/update", handleUpdate);
  server.on("/test", handleTest);
  server.begin();
}

void loop() {
  server.handleClient();
}

void handleIndex() {
  server.send(200, "text/plain", String(cadena)); //envia el último valor redibido por el sensor.
}

void handleUpdate() {
  sensor_value1 = server.arg("value1").toFloat();
  Serial.println(sensor_value1);
  server.send(200, "text/plain", "Updated");
}

void handleTest() {
  sensor_value1 = server.arg("value1").toFloat();
  sensor_value2 = server.arg("value2").toFloat();
  sensor_value3 = server.arg("value3").toInt();
  concatenar3valores(sensor_value1, sensor_value2, sensor_value3);
  server.send(200, "text/plain", String(cadena));
}

String concatenar3valores(float value1, float value2, int value3) {
  cadena = "";
  cadena += "Temperatura: ";
  cadena += value1;
  cadena += "C / Humedad: ";
  cadena += value2;
  cadena += "% / Alarma: ";
  cadena += value3;
  return cadena;
}
