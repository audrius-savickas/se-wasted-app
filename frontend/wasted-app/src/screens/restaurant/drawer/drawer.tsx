import React, {useEffect, useState} from "react"
import {StyleSheet} from "react-native"
import {Avatar, Colors, Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {getRestaurantById} from "../../../api"
import {Restaurant} from "../../../api/interfaces"
import {useRestaurant} from "../../../hooks/use-restaurant"
import {setHomeRoot} from "../../../services/navigation"
import {DrawerProps} from "./interfaces"

export const Drawer = ({componentId}: DrawerProps) => {
  const {restaurantId, logOutRestaurant} = useRestaurant()

  const [restaurant, setRestaurant] = useState({} as Restaurant)

  const fetchCustomer = async () => {
    try {
      const restaurant = await getRestaurantById({idRestaurant: restaurantId})
      if (restaurant) {
        setRestaurant(restaurant)
      }
    } catch (e) {
      console.error(e)
    }
  }

  const logOut = () => {
    logOutRestaurant()
    setHomeRoot()
  }

  const openProfile = () => {
    // showCustomerProfileModal({componentId, customer})
  }

  useEffect(() => {
    fetchCustomer()
  }, [])

  return (
    <View useSafeArea flex>
      <View marginL-s4 marginR-s4>
        <View centerV row>
          <Avatar size={50} backgroundColor={Colors.purple40} source={{uri: restaurant.imageURL}} />
          <View flex marginL-s2 marginR-s2 right>
            <Text text60L>{restaurant.name}</Text>
            <Text text90L>umarasss@gmail.com</Text>
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
