import {NativeStackScreenProps} from "@react-navigation/native-stack"
import React from "react"
import {Text, View} from "react-native-ui-lib"
import {FoodsList} from "../../components/foods-list"
import {RootStackParamList} from "../RootStackParamsList"

type foodListProps = NativeStackScreenProps<RootStackParamList, "FoodList">

export const FoodList = ({route}: foodListProps) => {
  const foods = [
    {id: "0", name: "Saltibarsciai", price: "2.49"},
    {id: "1", name: "Kebabas", price: "5.99"},
    {id: "2", name: "Kijevo kotletas su bulvių koše ir daržovėmis", price: "6.49"},
    {id: "3", name: "Cepelinai", price: "5.00"}
  ]

  return (
    <>
      <View center margin-s3>
        <Text text40L>Foods made by: "{route.params.restaurantName}"</Text>
      </View>
      <View flex>
        <FoodsList foods={foods} />
      </View>
    </>
  )
}
