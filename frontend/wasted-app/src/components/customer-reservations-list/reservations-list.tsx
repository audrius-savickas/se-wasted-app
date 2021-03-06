import React, {useState} from "react"
import {ListRenderItemInfo} from "react-native"
import {Text, View} from "react-native-ui-lib"
import {getCustomerReservedFoods} from "../../api/customer"
import {Food} from "../../api/interfaces"
import {useCustomer} from "../../hooks/use-customer"
import {navigateToFoodInfo} from "../../services/navigation"
import {CustomerReservationItem} from "../customer-reservation-item"
import {SimpleFoodsList} from "../simple-foods-list"
import {ReservationsListProps} from "./interfaces"

export const ReservationsList = ({componentId, foods}: ReservationsListProps) => {
  const {customerId} = useCustomer()
  const [reservations, setReservations] = useState(foods)
  const [refreshing, setRefreshing] = useState(false)

  const renderItem = ({item}: ListRenderItemInfo<Food>) => {
    return (
      <CustomerReservationItem food={item} onPress={() => navigateToFoodInfo(componentId, {componentId, food: item})} />
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
          <View flex center>
            <Text>You haven't made any reservations yet!</Text>
          </View>
        }
        refreshing={refreshing}
        renderItem={renderItem}
        onRefresh={onRefresh}
      />
    </View>
  )
}
