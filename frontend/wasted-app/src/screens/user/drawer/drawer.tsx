import React from "react"
import {StyleSheet} from "react-native"
import {Avatar, Colors, Image, Text, TouchableOpacity, View} from "react-native-ui-lib"

export const Drawer = ({componentId}) => {
  return (
    <View useSafeArea flex>
      <View marginL-s4 marginR-s4>
        <View centerV row>
          <Avatar size={50} backgroundColor={Colors.purple40} name={"Audrius Savickas"} />
          <View flex marginL-s2 marginR-s2 right>
            <Text text60L>Audrius Savickas</Text>
            <Text text90L>umarasss@gmail.com</Text>
          </View>
        </View>
        <View marginT-s2></View>
      </View>
      <View style={[styles.topBorder, styles.bottomBorder]}>
        <View marginL-s4 marginR-s4 marginV-s4 centerV>
          <TouchableOpacity row marginL-s2 marginR-s2 center>
            <Image source={require("../../../../assets/profile-30x30.png")} />
            <Text marginL-s2 text60L>
              Edit profile
            </Text>
          </TouchableOpacity>
        </View>
      </View>
      <View style={[styles.bottomBorder]}>
        <View marginL-s4 marginR-s4 marginV-s4 centerV>
          <TouchableOpacity row marginL-s2 marginR-s2 center>
            <Image tintColor={Colors.red10} source={require("../../../../assets/log-out-30x30.png")} />
            <Text marginL-s2 text60L red10>
              Log out
            </Text>
          </TouchableOpacity>
        </View>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  topBorder: {
    borderTopColor: Colors.grey50,
    borderTopWidth: 1
  },
  bottomBorder: {
    borderBottomColor: Colors.grey50,
    borderBottomWidth: 1
  }
})
