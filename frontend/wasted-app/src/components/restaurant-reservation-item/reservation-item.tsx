import moment from "moment"
import React, {useEffect, useState} from "react"
import {Colors, Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {getRestaurantById} from "../../api"
import {getCustomerById} from "../../api/customer"
import {Customer, Restaurant} from "../../api/interfaces"
import {useLocation} from "../../hooks/use-location"
import {formatPrice} from "../../utils/currency"
import {ReservationItemProps} from "./interfaces"

export const ReservationItem = ({food, onPress}: ReservationItemProps) => {
  const [restaurant, setRestaurant] = useState({} as Restaurant)
  const [customer, setCustomer] = useState({} as Customer)
  const [timeLeft, setTimeLeft] = useState(0)
  const {location} = useLocation()

  const {name, idRestaurant, imageURL, reservation} = food

  const fetchRestaurant = async () => {
    setRestaurant(
      await getRestaurantById({idRestaurant, coordinates: {latitude: location.latitude, longitude: location.longitude}})
    )
  }

  const fetchCustomer = async () => {
    setCustomer(await getCustomerById({customerId: food.reservation?.customerId as string}))
  }

  useEffect(() => {
    fetchRestaurant()
    fetchCustomer()

    setTimeLeft(Math.round(moment(reservation?.reservedAt).add(30, "minutes").diff(moment()) / 1000 / 60))
    const interval = setInterval(() => {
      setTimeLeft(Math.round(moment(reservation?.reservedAt).add(30, "minutes").diff(moment()) / 1000 / 60))
    }, 60000)

    return () => clearInterval(interval)
  }, [])

  return (
    <View br40 flex marginH-s8 marginV-s1 style={{borderColor: Colors.grey40, borderWidth: 1}}>
      <View row centerV>
        <View flex>
          <Text text50R purple30 marginV-s2 marginH-s3>
            {name}
          </Text>
        </View>
        <View>
          <Text text60L marginH-s3>
            {`${customer.firstName} ${customer.lastName}`}
          </Text>
        </View>
      </View>
      <Image source={{uri: imageURL}} style={{height: 130}} />
      <View row margin-s4 centerV>
        <View flex row centerV>
          <Text text60L>{formatPrice(reservation?.price as number)}</Text>
        </View>
        <View center row marginR-s4>
          <Image source={require("../../../assets/time-left-25x25.png")} style={{width: 20, height: 20}} />
          <Text marginL-s1>{timeLeft} mins left</Text>
        </View>
        <TouchableOpacity onPress={onPress}>
          <Text text60R purple30>
            SEE DETAILS
          </Text>
        </TouchableOpacity>
      </View>
    </View>
  )
}
