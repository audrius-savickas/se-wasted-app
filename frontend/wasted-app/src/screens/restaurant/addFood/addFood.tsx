import React, {useEffect, useState} from "react"
import {Navigation} from "react-native-navigation"
import {Button, Colors, Text, View, Wizard} from "react-native-ui-lib"
import {DecreaseType, Food} from "../../../api/interfaces"
import {useRestaurant} from "../../../hooks/use-restaurant"
import {setHomeRoot} from "../../../services/navigation"
import {AddFoodScreenProps} from "./interfaces"
import {BaseInfo} from "./Wizard.Steps/baseInfo"
import {FinalStep} from "./Wizard.Steps/finalStep"
import {PriceDecreasing} from "./Wizard.Steps/priceDecreasing"

const completedStepIndex = 3

export const AddFood = ({existingFood}: AddFoodScreenProps) => {
  const {restaurantId} = useRestaurant()
  const [activeIndex, setActiveIndex] = useState<number>(0)
  const [food, setFood] = useState<Food>(
    existingFood || {
      id: "0",
      name: "",
      description: "",
      idRestaurant: restaurantId,
      startingPrice: 0,
      minimumPrice: 0,
      currentPrice: 0.5,
      createdAt: new Date().toDateString(),
      typesOfFood: [],
      startDecreasingAt: new Date().toDateString(),
      decreaseType: DecreaseType.AMOUNT,
      intervalTimeInMinutes: 0,
      amountPerInterval: 0,
      percentPerInterval: 0,
      imageURL: "",
      reservation: null
    }
  )

  useEffect(() => {
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "LOG_OUT") {
        setHomeRoot()
      }
    })
    return () => listener.remove()
  }, [])

  const getStepState = (n: Number) => {
    if (n === activeIndex) return Wizard.States.ENABLED

    return n > activeIndex ? Wizard.States.DISABLED : Wizard.States.COMPLETED
  }

  const renderCurrentStep = () => {
    return (
      <View flex marginT-s10>
        {renderStep(activeIndex)}
      </View>
    )
  }

  const renderStep = (n: number) => {
    switch (n) {
      case 0:
        return <BaseInfo food={food} setFood={setFood} />
      case 1:
        return <PriceDecreasing food={food} setFood={setFood} />
      case 2:
        return <FinalStep food={food} setFood={setFood} />
    }
  }

  return (
    <View flex margin-s3>
      <Text center text40M marginT-10>
        Add new meal
      </Text>
      <Wizard testID={"addFood.wizard"} activeIndex={activeIndex}>
        <Wizard.Step state={getStepState(0)} label={"Fill basic information"} />
        <Wizard.Step state={getStepState(1)} label={"Fill price decreasing"} />
        <Wizard.Step state={getStepState(2)} label={"Add food"} />
      </Wizard>

      <View style={{height: "70%"}}>{renderCurrentStep()}</View>

      <View
        flex
        style={{
          flexDirection: "row",
          alignItems: "flex-end",
          width: "100%",
          height: "30%",
          justifyContent: "space-evenly"
        }}
      >
        <Button
          backgroundColor={Colors.blue10}
          label="Previous step"
          disabled={activeIndex === 0}
          onPress={() => setActiveIndex(activeIndex - 1)}
        />
        <Button
          backgroundColor={Colors.green10}
          label="Next"
          disabled={activeIndex + 1 === completedStepIndex}
          onPress={() => setActiveIndex(activeIndex + 1)}
        />
      </View>
    </View>
  )
}
