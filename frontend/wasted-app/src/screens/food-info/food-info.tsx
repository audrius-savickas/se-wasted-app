import moment from "moment"
import React, {useEffect, useState} from "react"
import {Alert, StyleSheet} from "react-native"
import {ScrollView} from "react-native-gesture-handler"
import {Navigation} from "react-native-navigation"
import {Button, Chip, Colors, ExpandableSection, Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {getRestaurantById} from "../../api"
import {DecreaseType, Restaurant} from "../../api/interfaces"
import {cancelFoodReservation, reserveFood} from "../../api/reservation"
import {PriceIndicator} from "../../components/price-indicator"
import {useCustomer} from "../../hooks/use-customer"
import {useLocation} from "../../hooks/use-location"
import {navigateToRestaurantInfo} from "../../services/navigation"
import {formatPrice} from "../../utils/currency"
import {convertMinsToHrsMins, formatDate, formatTime, timeAgoFull} from "../../utils/date"
import {FoodInfoProps} from "./interfaces"

export const FoodInfo = ({componentId, food, showRestaurantLink = true}: FoodInfoProps) => {
  const [restaurant, setRestaurant] = useState({} as Restaurant)
  const [descriptionExpanded, setDescriptionExpanded] = useState(true)
  const [reservation, setReservation] = useState(food.reservation)
  const [timeLeft, setTimeLeft] = useState(0)
  const {location} = useLocation()
  const {customerId} = useCustomer()

  const {
    name,
    description,
    typesOfFood,
    decreaseType,
    intervalTimeInMinutes,
    currentPrice,
    startingPrice,
    minimumPrice,
    idRestaurant,
    createdAt,
    amountPerInterval,
    percentPerInterval,
    startDecreasingAt,
    imageURL,
    id
  } = food

  const fetchRestaurant = async () => {
    setRestaurant(
      (await getRestaurantById({
        idRestaurant,
        coordinates: {latitude: location.latitude, longitude: location.longitude}
      })) as Restaurant
    )
  }

  const makeReservation = async () => {
    const response = await reserveFood({foodId: id, customerId})
    if (response) {
      console.log("reservation made")
      setReservation({reservedAt: new Date(), foodId: food.id, customerId, price: currentPrice})
    } else {
      console.log("reservation failed")
    }
  }

  const cancelReservation = async () => {
    const response = await cancelFoodReservation({foodId: id, customerId})
    if (response) {
      console.log("reservation cancelled")
      setReservation(null)
    } else {
      console.log("reservation cancellation failed")
    }
  }

  const makeReservationAlert = () => {
    Alert.alert(
      "Make reservation?",
      "If you don't pick it up in 30 minutes, you will receive penalty points to your account.",
      [{text: "OK", onPress: makeReservation}, {text: "Cancel"}]
    )
  }

  const cancelReservationAlert = () => {
    Alert.alert(
      "Cancel reservation?",
      "If you cancel this reservation, you won't be able to pick up this food unless reserved again.",
      [{text: "OK", onPress: cancelReservation}, {text: "Cancel"}]
    )
  }

  useEffect(() => {
    if (reservation) {
      setTimeLeft(Math.round(moment(reservation.reservedAt).add(30, "minutes").diff(moment()) / 1000 / 60))
      const interval = setInterval(() => {
        setTimeLeft(Math.round(moment(reservation.reservedAt).add(30, "minutes").diff(moment()) / 1000 / 60))
      }, 60000)

      return () => clearInterval(interval)
    }
  }, [reservation])

  useEffect(() => {
    fetchRestaurant()

    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "DISMISS") {
        if (!showRestaurantLink) {
          Navigation.dismissModal(componentId)
        }
      }
    })
    return () => listener.remove()
  }, [])

  return (
    <ScrollView>
      <View margin-s4 marginB-s2>
        <View centerH>
          <Text text30M purple20 marginT-s2 marginB-s1>
            {name}
          </Text>
          <View row centerH style={{flexWrap: "wrap"}}>
            {typesOfFood.map(type => (
              <Chip margin-s1 key={type.id} label={type.name} />
            ))}
          </View>
          <View style={styles.shadow}>
            <Image
              marginT-s2
              source={{uri: imageURL, height: 220, width: 350}}
              style={{
                height: 220,
                width: 350
              }}
            />
          </View>
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
          <View marginT-s2>
            <PriceIndicator currentPrice={currentPrice} minimumPrice={minimumPrice} maximumPrice={startingPrice} />
            <View marginT-s2>
              <Text text80L>
                The price of <Text text80M>{name}</Text> decreases by{" "}
                <Text text80M>
                  {decreaseType === DecreaseType.AMOUNT ? formatPrice(amountPerInterval) : `${percentPerInterval}%`}
                </Text>{" "}
                every <Text text80M>{convertMinsToHrsMins(intervalTimeInMinutes)}</Text> starting at{" "}
                <Text text80M>{`${formatDate(startDecreasingAt)}, ${formatTime(startDecreasingAt)}`}</Text>.
              </Text>
            </View>
          </View>
          <View centerV marginT-s4>
            <ExpandableSection
              marginB-s10
              sectionHeader={
                <View row>
                  <Text text60L purple20 marginR-s2>
                    Description
                  </Text>
                  <Image
                    style={{
                      alignSelf: "center",
                      width: 16,
                      height: 16,
                      transform: [{scaleY: descriptionExpanded ? -1 : 1}]
                    }}
                    source={require("../../../assets/down-chevron.png")}
                  />
                </View>
              }
              expanded={descriptionExpanded}
              onPress={() => setDescriptionExpanded(!descriptionExpanded)}
            >
              <Text marginT-s1 text80L>
                {description}
              </Text>
            </ExpandableSection>
          </View>
          <View centerV marginT-s4>
            <Text marginB-s2 text60L purple20 style={{width: 120}}>
              Reservation
            </Text>
            {reservation ? (
              <>
                <Text center text70L>
                  You have reserved this food at:
                </Text>
                <Text center text70M>
                  {formatDate(reservation.reservedAt.toString())}, {formatTime(reservation.reservedAt.toString())}.
                </Text>
                <Text center marginT-s2 text70L>
                  Time left to pick up your reservation:
                </Text>
                <Text center text70M>
                  {timeLeft} minutes.
                </Text>
                <Text center marginT-s2 text70L>
                  You may cancel your reservation at any time with the button below.
                </Text>
                <Button marginV-s3 label="Cancel Reservation" style={styles.button} onPress={cancelReservationAlert} />
              </>
            ) : (
              <>
                <Text center text70L>
                  This food is not reserved yet. Press the button below to reserve it for 30 minutes.
                </Text>
                <Button marginV-s3 label="Reserve" style={styles.button} onPress={makeReservationAlert} />
              </>
            )}
          </View>
        </View>
      </View>
    </ScrollView>
  )
}

const styles = StyleSheet.create({
  shadow: {
    shadowColor: Colors.black,
    shadowOpacity: 0.4,
    shadowOffset: {height: 0, width: 0}
  },
  button: {
    // position: "absolute",
    alignSelf: "center"
    // bottom: 0
  }
})
