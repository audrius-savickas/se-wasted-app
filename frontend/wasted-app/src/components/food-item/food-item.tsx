import React from "react"
import {StyleSheet} from "react-native"
import {Chip, Colors, Image, ListItem, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {formatPrice} from "../../utils/currency"
import {FoodItemProps} from "./interfaces"

export const FoodItem = ({food, onPress}: FoodItemProps) => {
  const {name, currentPrice, startingPrice, typesOfFood, imageURL} = food
  return (
    <ListItem height="auto">
      <TouchableOpacity
        flex
        paddingV-s3
        style={{
          borderTopColor: Colors.black,
          borderTopWidth: StyleSheet.hairlineWidth
        }}
        onPress={onPress}
      >
        <View row centerV marginH-s4>
          <View marginR-s4>
            <Image source={{uri: imageURL, width: 80, height: 80}} />
          </View>
          <ListItem.Part middle>
            <View>
              <Text text50L>{name}</Text>
              <View row style={{flexWrap: "wrap"}}>
                {typesOfFood.map(type => (
                  <Chip marginR-s1 marginV-s1 key={type.id} label={type.name} size={25} />
                ))}
              </View>
            </View>
          </ListItem.Part>
          <ListItem.Part right marginL-s4>
            <View center>
              <Text text60L green10>
                {formatPrice(currentPrice)}
              </Text>
              <Text text70L red10 style={{textDecorationLine: "line-through"}}>
                {formatPrice(startingPrice)}
              </Text>
            </View>
          </ListItem.Part>
        </View>
      </TouchableOpacity>
    </ListItem>
  )
}
