import React, {useEffect, useState} from "react"
import {Chip, Image, Text, View} from "react-native-ui-lib"
import {getRestaurantById} from "../../api"
import {Restaurant} from "../../api/interfaces"
import {formatPrice} from "../../utils/currency"
import {formatDate, formatTime, timeAgoFull} from "../../utils/date"
import {FoodInfoProps} from "./interfaces"

export const FoodInfo = ({food}: FoodInfoProps) => {
  const [restaurant, setRestaurant] = useState({} as Restaurant)

  const {name, typesOfFood, currentPrice, idRestaurant, createdAt, imageURL} = food

  const fetchRestaurant = async () => {
    setRestaurant(await getRestaurantById(idRestaurant))
  }

  useEffect(() => {
    fetchRestaurant()
  }, [])

  return (
    <View margin-s4>
      <View centerH>
        <Text text30M purple20 marginT-s2 marginB-s1>
          {name}
        </Text>
        <View row centerH style={{flexWrap: "wrap"}}>
          {typesOfFood.map(type => (
            <Chip margin-s1 key={type.id} label={type.name} />
          ))}
        </View>
        <Image marginT-s2 source={{uri: imageURL, height: 200, width: 330}} />
      </View>
      <View marginT-s6 marginH-s6>
        <View row centerV>
          <Text text60L purple20 style={{width: 120}}>
            Price
          </Text>
          <Text text60L>{formatPrice(currentPrice)}</Text>
        </View>
        <View row centerV marginT-s4>
          <Text text60L purple20 style={{width: 120}}>
            Cooked
          </Text>
          <View>
            <Text text60L style={{width: 200}}>
              {`${timeAgoFull(createdAt)}`}
            </Text>
            <Text text80L style={{width: 250}}>{`${formatDate(createdAt)} | ${formatTime(createdAt)}`}</Text>
          </View>
        </View>
        <View row centerV marginT-s4>
          <Text text60L purple20 style={{width: 120}}>
            Restaurant
          </Text>
          <Text text60L>{restaurant.name}</Text>
        </View>
      </View>
    </View>
  )
}
