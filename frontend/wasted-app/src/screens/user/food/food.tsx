import React, {useEffect, useState} from "react"
import {Navigation} from "react-native-navigation"
import {Colors, LoaderScreen, Text, View} from "react-native-ui-lib"
import {getAllFood} from "../../../api/food"
import {Food as IFood} from "../../../api/interfaces"
import {FoodsList} from "../../../components/foods-list"
import {setHomeRoot} from "../../../services/navigation"
import {HOME_BUTTON} from "../home-button"
import {FoodProps} from "./interfaces"

export const Food = ({componentId}: FoodProps) => {
  const [loading, setLoading] = useState(true)
  const [foods, setFoods] = useState([] as IFood[])

  const fetchFoods = async () => {
    setLoading(true)
    setFoods(await getAllFood())
    setLoading(false)
  }

  useEffect(() => {
    fetchFoods()
    Navigation.mergeOptions(componentId, {topBar: {leftButtons: [HOME_BUTTON]}})
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "GO_BACK") {
        setHomeRoot()
      }
    })
    return () => listener.remove()
  }, [])

  return (
    <View flex>
      {loading ? (
        <LoaderScreen color={Colors.blue30} message="Loading..." />
      ) : (
        <FoodsList foods={foods} componentId={componentId} />
      )}
    </View>
  )
}
