import React, {useEffect, useState} from "react"
import {Colors, Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {getRestaurantById} from "../../api"
import {Restaurant} from "../../api/interfaces"
import {formatPrice} from "../../utils/currency"
import {timeAgoFull} from "../../utils/date"
import {FoodItemProps} from "./interfaces"

export const FoodItem = ({food, onPress}: FoodItemProps) => {
  const {name, idRestaurant, currentPrice, startingPrice, imageURL, createdAt} = food
  const [restaurant, setRestaurant] = useState({} as Restaurant)

  const fetchRestaurant = async () => {
    setRestaurant(await getRestaurantById(idRestaurant))
  }

  useEffect(() => {
    fetchRestaurant()
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
            {restaurant.name}
          </Text>
        </View>
      </View>
      <Image source={{uri: imageURL}} style={{height: 130}} />
      <View row margin-s4 centerV>
        <View flex row centerV>
          <Text text60L>{formatPrice(currentPrice)}</Text>
          {currentPrice < startingPrice ? (
            <Image
              source={require("../../../assets/low-price.png")}
              style={{height: 25, resizeMode: "contain", left: -15}}
            />
          ) : null}
        </View>
        <View marginR-s6>
          <Text>{timeAgoFull(createdAt)}</Text>
        </View>
        <TouchableOpacity onPress={onPress}>
          <Text text60R purple30>
            SEE MORE
          </Text>
        </TouchableOpacity>
      </View>
    </View>
  )
}
