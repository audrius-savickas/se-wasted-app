import React, {useEffect, useState} from "react"
import {Assets, Chip, Colors, Image, ProgressBar, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {getRestaurantById} from "../../api"
import {Restaurant} from "../../api/interfaces"
import {PriceIndicator} from "../../components/price-indicator"
import {navigateToRestaurantInfo} from "../../services/navigation"
import {formatPrice} from "../../utils/currency"
import {formatDate, formatTime, timeAgoFull} from "../../utils/date"
import {FoodInfoProps} from "./interfaces"

export const FoodInfo = ({componentId, food, showRestaurantLink = true}: FoodInfoProps) => {
  const [restaurant, setRestaurant] = useState({} as Restaurant)

  const {name, typesOfFood, currentPrice, startingPrice, minimumPrice, idRestaurant, createdAt, imageURL} = food

  const fetchRestaurant = async () => {
    setRestaurant((await getRestaurantById(idRestaurant)) as Restaurant)
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
        <Image marginT-s2 source={{uri: imageURL, height: 200, width: 330}} style={{height: 200, width: 300}} />
      </View>
      <View marginT-s6 marginH-s6>
        {showRestaurantLink ? (
          <TouchableOpacity
            row
            centerV
            onPress={() => {
              navigateToRestaurantInfo(componentId, {componentId, restaurant})
            }}
          >
            <Text text60L purple20 style={{width: 120}}>
              Restaurant
            </Text>
            <Text text60L>{`${restaurant.name} ↗️`}</Text>
          </TouchableOpacity>
        ) : (
          <View row centerV>
            <Text text60L purple20 style={{width: 120}}>
              Restaurant
            </Text>
            <Text text60L>{restaurant.name}</Text>
          </View>
        )}
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
            Price
          </Text>
          <Text text60L green10={currentPrice !== startingPrice} text60M={currentPrice !== startingPrice}>
            {formatPrice(currentPrice)}
          </Text>
        </View>
        <View marginT-s3>
          <PriceIndicator currentPrice={currentPrice} minimumPrice={minimumPrice} maximumPrice={startingPrice} />
          {/* <Text></Text> */}
        </View>
      </View>
    </View>
  )
}
