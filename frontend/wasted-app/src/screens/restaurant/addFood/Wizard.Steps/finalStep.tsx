import React, {useState} from "react"
import {View, Button} from "react-native-ui-lib"
import {addNewFood} from "../../../../api/food"
import {Props} from "./interfaces"

export const FinalStep = ({food, setFood}: Props) => {
  const [loading, setLoading] = useState(false)

  const saveMeal = async () => {
    setLoading(true)
    await addNewFood({
      ...food,
      startingPrice: food.currentPrice,
      createdAt: new Date().toISOString(),
      startDecreasingAt: new Date(food.startDecreasingAt).toISOString()
    })
    setLoading(false)
  }

  return (
    <View
      flex
      style={{
        width: "100%"
      }}
    >
      <Button disabled={loading} label="Save" onPress={saveMeal} />
    </View>
  )
}
