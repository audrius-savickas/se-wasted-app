import React, {useEffect, useState} from "react"
import {StyleSheet} from "react-native"
import {Avatar, Colors, Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {getCustomerById} from "../../../api/customer"
import {Customer} from "../../../api/interfaces"
import {useCustomer} from "../../../hooks/use-customer"
import {setHomeRoot, showCustomerProfileModal} from "../../../services/navigation"
import {DrawerProps} from "./interfaces"

export const Drawer = ({componentId}: DrawerProps) => {
  const {customerId, logOutCustomer} = useCustomer()

  const [customer, setCustomer] = useState({} as Customer)

  const fetchCustomer = async () => {
    try {
      console.log(customerId)
      const customer = await getCustomerById({customerId})
      if (customer) {
        setCustomer(customer)
      }
    } catch (e) {
      console.error(e)
    }
  }

  const logOut = () => {
    logOutCustomer()
    setHomeRoot()
  }

  const openProfile = () => {
    showCustomerProfileModal({componentId, customer})
  }

  useEffect(() => {
    fetchCustomer()
  }, [])

  return (
    <View useSafeArea flex>
      <View marginL-s4 marginR-s4>
        <View centerV row>
          <Avatar size={50} backgroundColor={Colors.purple40} name={`${customer.firstName} ${customer.lastName}`} />
          <View flex marginL-s2 marginR-s2 right>
            <Text text60L>{`${customer.firstName} ${customer.lastName}`}</Text>
            <Text text90L>{customer.mail}</Text>
          </View>
        </View>
        <View marginT-s2></View>
      </View>
      <View style={[styles.topBorder, styles.bottomBorder]}>
        <View marginL-s4 marginR-s4 marginV-s4 centerV>
          <TouchableOpacity row marginL-s2 marginR-s2 center onPress={openProfile}>
            <Image source={require("../../../../assets/profile-30x30.png")} />
            <Text marginL-s2 text60L>
              Edit profile
            </Text>
          </TouchableOpacity>
        </View>
      </View>
      <View style={[styles.bottomBorder]}>
        <View marginL-s4 marginR-s4 marginV-s4 centerV>
          <TouchableOpacity row marginL-s2 marginR-s2 center onPress={logOut}>
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
