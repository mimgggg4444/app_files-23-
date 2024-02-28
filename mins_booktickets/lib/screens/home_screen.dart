import 'package:fluentui_icons/fluentui_icons.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:gap/gap.dart';
import 'package:mins_booktickets/screens/hotel_screen.dart';
import 'package:mins_booktickets/screens/ticket_view.dart';
import 'package:mins_booktickets/utils/app_styles.dart';


class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Styles.bgColor,
      body: ListView(
        children: [
          Container(

            padding: const EdgeInsets.symmetric(horizontal: 30),
            child: Column(
              children: [
                const Gap(50),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,

                  children: [
                    Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [

                        Text(
                            '어서오세요!', style: Styles.headLineStyle3,
                        ),
                        const Gap(5),
                        Text(
                            '티켓 예매하러 가기', style: Styles.headLineStyle1,
                        ),
                      ],
                    ),
                    Container(
                      height: 50,
                      width: 50,
                      decoration: BoxDecoration(
                        borderRadius: BorderRadius.circular(10),
                        image: const DecorationImage(
                          fit: BoxFit.fitHeight,
                          image: AssetImage(
                              "asserts/images/img_1.png"
                          )
                        )
                      ),
                    ),
                  ],
                ),
                const Gap(25),
                Container(
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(10),
                    color: const Color(0xfff4f6fd)
                  ),
                  padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 12),
                  child: Row(
                    children: [
                      const Icon(FluentSystemIcons.ic_fluent_search_regular, color: Color(0xffbfc205),),
                      Text('검색하기', style: Styles.headLineStyle4,),
                    ],
                  ),
                ),


                const Gap(40),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text('도착하는 비행기 확인', style: Styles.headLineStyle2,),
                    InkWell(
                        onTap: (){
                          print('클릭하셨습니다.');
                        },
                        child: Text('모두 보기', style: Styles.textStyle.copyWith(color: Styles.primaryColor),)),
                  ],
                )
              ],
            ),
          ),
          const Gap(15),
          SingleChildScrollView(
            scrollDirection: Axis.horizontal,
            padding: const EdgeInsets.only(left: 15),
            child: Row(
              children: [
                TicketView(),
                TicketView(),
              ],
            ),
          ),
          const Gap(15),
          Container(
            padding: const EdgeInsets.symmetric(horizontal: 30),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Text('호텔 예약 확인', style: Styles.headLineStyle2,),
                InkWell(
                    onTap: (){
                      print('클릭하셨습니다.');
                    },
                    child: Text('모두 보기', style: Styles.textStyle.copyWith(color: Styles.primaryColor),)),
              ],
            ),
          ),

          SingleChildScrollView(
            scrollDirection: Axis.horizontal,
              padding: const EdgeInsets.only(left: 20),
              child: Row(
                children: [
                  HotelScreen(),
                  HotelScreen(),
                  HotelScreen(),
                  HotelScreen(),

                ],

              ),
          ),

        ],
      ),
    );
  }
}
