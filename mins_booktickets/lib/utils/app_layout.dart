import 'package:flutter/cupertino.dart';


class AppLayout{

  // 이게 뜻하는게 뭔가?
  static getSize(BuildContext context){
    return MediaQuery.of(context).size;
  }
}