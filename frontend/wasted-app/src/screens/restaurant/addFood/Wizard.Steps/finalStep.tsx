import moment from "moment"
import React, {useState} from "react"
import {Alert} from "react-native"
import {Button, Text, View} from "react-native-ui-lib"
import {addNewFood} from "../../../../api/food"
import {Props} from "./interfaces"

export const FinalStep = ({food}: Props) => {
  const [loading, setLoading] = useState(false)

  const saveMeal = async () => {
    setLoading(true)
    await addNewFood({
      ...food,
      startingPrice: food.currentPrice,
      createdAt: moment().add(2, "hours").toISOString(),
      startDecreasingAt: moment(food.startDecreasingAt).add(2, "hours").toISOString()
    })
    setLoading(false)
    Alert.alert("Food successfully added!", "Please refresh the food list to see it added.")
  }

  return (
    <View flex center>
      <Text text50L marginB-s2>
        Add your food!
      </Text>
      <Button disabled={loading} label="Save" onPress={saveMeal} />
    </View>
  )
}
