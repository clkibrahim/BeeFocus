import 'package:bee_focus/screens/login_screen.dart';
import 'package:flutter/material.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      // 1. Uygulamanın sistemdeki adını ayarladık.
      title: 'beefocus',
      
      // 2. Sağ üstteki "Debug" etiketini kaldırdık.
      debugShowCheckedModeBanner: false, 
      
      theme: ThemeData(
        primarySwatch: Colors.deepPurple, // Ana rengimizi seçtik (daha sonra özelleştirebiliriz)
        visualDensity: VisualDensity.adaptivePlatformDensity,
        fontFamily: 'Poppins', // (Opsiyonel) Projeye güzel bir font ekleyebiliriz.
      ),
      
      // 3. Başlangıç ekranını ayarlayacağız.
      home: const LoginScreen()
    );
  }
}