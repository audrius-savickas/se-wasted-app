import React, {useEffect} from "react"
import {Alert, Linking, StyleSheet} from "react-native"
import {Navigation} from "react-native-navigation"
import {Avatar, Colors, Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {callNumber} from "../../../utils/call"
import {CustomerInfoProps} from "./interfaces"

export const CustomerInfo = ({componentId, customer}: CustomerInfoProps) => {
  useEffect(() => {
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "DISMISS") {
        Navigation.dismissModal(componentId)
      }
    })
    return () => listener.remove()
  }, [])

  const emailCustomer = async () => {
    const url = `mailto:${customer.mail}`
    if (await Linking.canOpenURL(url)) {
      await Linking.openURL(`mailto:${customer.mail}`)
    } else {
      console.log("hi")
      Alert.alert("Email is not available.")
    }
  }

  return (
    <View flex center marginH-s8>
      <View marginT-s2 center>
        <Avatar size={100} backgroundColor={Colors.purple40} name={`${customer.firstName} ${customer.lastName}`} />
        <Text marginT-s2 text60M>{`${customer.firstName} ${customer.lastName}`}</Text>
      </View>
      <View marginT-s8>
        <Text text60L>Contact Info</Text>
        <TouchableOpacity
          bg-white
          row
          centerV
          marginT-s2
          paddingV-s2
          paddingH-s6
          style={styles.contact}
          onPress={emailCustomer}
        >
          <Image source={require("../../../../assets/mail-30x30.png")} />
          <Text marginL-s4 text70R>{`Email: `}</Text>
          <Text text70L>{`${customer.mail}`}</Text>
        </TouchableOpacity>
        <TouchableOpacity
          bg-white
          row
          centerV
          marginT-s2
          paddingV-s2
          paddingH-s6
          style={styles.contact}
          onPress={() => callNumber(customer.phone)}
        >
          <Image source={require("../../../../assets/phone-30x30.png")} />
          <Text marginL-s4 text70R>{`Phone number: `}</Text>
          <Text text70L>{`${customer.phone}`}</Text>
        </TouchableOpacity>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  contact: {
    borderColor: Colors.grey40,
    borderWidth: 1,
    borderRadius: 20,
    shadowColor: Colors.black,
    shadowRadius: 2,
    shadowOffset: {width: 0, height: 0},
    shadowOpacity: 0.3
  },
  button: {
    width: 150,
    alignSelf: "center"
  },
  error: {position: "absolute", alignSelf: "center", width: "85%"}
})
