import React, {useState} from "react"
import {ListRenderItemInfo} from "react-native"
import {Text, View} from "react-native-ui-lib"
import {getCustomerReservedFoods} from "../../api/customer"
import {Food} from "../../api/interfaces"
import {useCustomer} from "../../hooks/use-customer"
import {navigateToFoodInfo} from "../../services/navigation"
import {RestaurantReservationItem} from "../restaurant-reservation-item"
import {SimpleFoodsList} from "../simple-foods-list"
import {ReservationsListProps} from "./interfaces"

export const ReservationsList = ({componentId, foods}: ReservationsListProps) => {
  const {customerId} = useCustomer()
  const [reservations, setReservations] = useState(foods)
  const [refreshing, setRefreshing] = useState(false)

  const renderItem = ({item}: ListRenderItemInfo<Food>) => {
    return (
      <RestaurantReservationItem
        food={item}
        onPress={() => navigateToFoodInfo(componentId, {componentId, food: item, showRestaurantLink: false})}
      />
    )
  }

  const onRefresh = () => {
    setRefreshing(true)
    fetchReservations()
  }

  const fetchReservations = async () => {
    const response = await getCustomerReservedFoods({customerId})
    if (response) {
      console.log("success")
      setReservations(response)
    } else {
      console.log("fail")
    }
    setRefreshing(false)
  }

  return (
    <View flex>
      <SimpleFoodsList
        foods={reservations}
        emptyListComponent={
          <View flex center marginH-s4>
            <Text center>Customers haven't made any reservations for your foods yet!</Text>
          </View>
        }
        refreshing={refreshing}
        renderItem={renderItem}
        onRefresh={onRefresh}
      />
    </View>
  )
}
