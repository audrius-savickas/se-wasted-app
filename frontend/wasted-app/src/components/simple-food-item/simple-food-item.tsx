import React from "react"
import {StyleSheet} from "react-native"
import {Chip, Colors, Image, ListItem, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {formatPrice} from "../../utils/currency"
import {SimpleFoodItemProps} from "./interfaces"

export const SimpleFoodItem = ({food, onPress}: SimpleFoodItemProps) => {
  const {name, currentPrice, startingPrice, typesOfFood, imageURL} = food

  const renderPrice = () => {
    if (startingPrice === currentPrice) {
      return (
        <Text text60L grey10>
          {formatPrice(currentPrice)}
        </Text>
      )
    }
    return (
      <View center>
        <Text text70L red30 style={{textDecorationLine: "line-through"}}>
          {formatPrice(startingPrice)}
        </Text>
        <Text text60L green10>
          {formatPrice(currentPrice)}
        </Text>
      </View>
    )
  }

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
            <Image source={{uri: imageURL, width: 80, height: 80}} style={{height: 80, width: 80}} />
          </View>
          <ListItem.Part middle>
            <View>
              <Text text50L>{name}</Text>
              <View row style={{flexWrap: "wrap"}}>
                {typesOfFood.map(type => (
                  <Chip
                    marginR-s1
                    marginV-s1
                    br60
                    labelStyle={{color: Colors.grey20, fontWeight: "400"}}
                    style={{borderColor: Colors.grey40, borderWidth: 1}}
                    key={type.id}
                    label={type.name}
                    size={25}
                  />
                ))}
              </View>
            </View>
          </ListItem.Part>
          <ListItem.Part right marginL-s4>
            <View center>{renderPrice()}</View>
          </ListItem.Part>
        </View>
      </TouchableOpacity>
    </ListItem>
  )
}
