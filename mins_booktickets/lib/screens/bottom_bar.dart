import 'package:fluentui_icons/fluentui_icons.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mins_booktickets/screens/home_screen.dart';

class BottomBar extends StatefulWidget {
  const BottomBar({super.key});

  @override
  State<BottomBar> createState() => _BottomBarState();
}

class _BottomBarState extends State<BottomBar> {
  int _seletedIndex=0;
  static final List<Widget>_widgetOptions =<Widget>[

    HomeScreen(),
    const Text("Search"),
    const Text("Ticket"),
    const Text("Profile")
  ];

  void _onItemTapped(int index){
    setState(() {
      _seletedIndex=index;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(

      body: Center(
        child: _widgetOptions[_seletedIndex],
      ),
      bottomNavigationBar: BottomNavigationBar(
        onTap: _onItemTapped,
        elevation: 10,
          showSelectedLabels: false,
          showUnselectedLabels: false,
          selectedItemColor: Colors.blueGrey,
          unselectedItemColor: Colors.amberAccent,
          type: BottomNavigationBarType.fixed,
          items: const [
            BottomNavigationBarItem(icon: Icon(FluentSystemIcons.ic_fluent_alert_snooze_regular),
                              activeIcon: Icon(FluentSystemIcons.ic_fluent_home_add_regular),
                              label:"Home"),
            BottomNavigationBarItem(icon: Icon(Icons.search), label:"Search"),
            BottomNavigationBarItem(icon: Icon(Icons.airplane_ticket), label:"Ticket"),
            BottomNavigationBarItem(icon: Icon(Icons.person), label:"Profile")
          ]),
    );
  }
}
